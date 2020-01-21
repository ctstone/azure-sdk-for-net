// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class AnalyzeClientTests
    {
        public AnalyzeClientTests()
        { }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetAnalysisResult_ReturnsAnalysis_On200(bool isAsync)
        {
            // Arrange
            var content = @"{ ""status"": ""running"" }";
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            var operationId = "fake-operation-id";
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act
            var response = isAsync
                ? await client.GetAnalysisResultAsync(operationId)
                : client.GetAnalysisResult(operationId);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.Equal(OperationStatus.Running, response.Value.Status);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetAnalysisResult_Throws_On400(bool isAsync)
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.BadRequest);
            var operationId = "fake-operation-id";
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<RequestFailedException>(() => client.GetAnalysisResultAsync(operationId))
                : Assert.Throws<RequestFailedException>(() => client.GetAnalysisResult(operationId));
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Theory]
        [InlineData(true, "")]
        [InlineData(false, "")]
        [InlineData(true, null)]
        [InlineData(false, null)]
        public async Task GetAnalysisResult_Throws_OnMissingOperationId(bool isAsync, string operationId)
        {
            // Arrange
            var client = GetClient();

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<ArgumentException>(() => client.GetAnalysisResultAsync(operationId))
                : Assert.Throws<ArgumentException>(() => client.GetAnalysisResult(operationId));
            Assert.Equal("operationId", ex.ParamName);
        }

        [Theory]
        [InlineData(true, typeof(Uri))]
        [InlineData(false, typeof(Uri))]
        [InlineData(true, typeof(Stream))]
        [InlineData(false, typeof(Stream))]
        public async Task StartAnalyze_ReturnsOperation_On202(bool isAsync, Type payloadType)
        {
            // Arrange
            var operationId = "fake-operation-id";
            var mockResponse = new MockResponse((int)HttpStatusCode.Accepted);
            mockResponse.AddHeader(new HttpHeader("Operation-Location", $"http://localhost/fake-model-id/{operationId}"));
            var client = GetClient(mockResponse);

            // Act
            AnalyzeOperation operation;
            if (payloadType == typeof(Stream))
            {
                var requestStream = new MemoryStream();
                var requestType = FormContentType.Pdf;
                operation = isAsync
                    ? await client.StartAnalyzeAsync(requestStream, requestType)
                    : client.StartAnalyze(requestStream, requestType);
            }
            else if (payloadType == typeof(Uri))
            {
                var uri = new Uri("http://localhost/fake/file");
                operation = isAsync
                    ? await client.StartAnalyzeAsync(uri)
                    : client.StartAnalyze(uri);
            }
            else
            {
                throw new NotSupportedException("Test payload type is not supported");
            }

            // Assert
            Assert.NotNull(operation);
            Assert.Null(operation.Value);
            Assert.NotNull(operation.Id);
            Assert.Equal(operationId, operation.Id);
            Assert.False(operation.HasCompleted);
            Assert.False(operation.HasValue);
        }

        [Theory]
        [InlineData(true, typeof(Uri))]
        [InlineData(false, typeof(Uri))]
        [InlineData(true, typeof(Stream))]
        [InlineData(false, typeof(Stream))]
        public async Task StartAnalyze_Throws_OnMissingParameter(bool isAsync, Type payloadType)
        {
            // Arrange
            var client = GetClient();

            // Act
            ArgumentNullException ex;
            if (payloadType == typeof(Stream))
            {
                ex = isAsync
                    ? await Assert.ThrowsAsync<ArgumentNullException>(() => client.StartAnalyzeAsync(null as Stream))
                    : Assert.Throws<ArgumentNullException>(() => client.StartAnalyze(null as Stream));
            }
            else if (payloadType == typeof(Uri))
            {
                ex = isAsync
                    ? await Assert.ThrowsAsync<ArgumentNullException>(() => client.StartAnalyzeAsync(null as Uri))
                    : Assert.Throws<ArgumentNullException>(() => client.StartAnalyze(null as Uri));
            }
            else
            {
                throw new NotSupportedException("Test payload type is not supported");
            }

            Assert.NotNull(ex.ParamName);
        }

        [Theory]
        [InlineData(true, typeof(Uri))]
        [InlineData(false, typeof(Uri))]
        [InlineData(true, typeof(Stream))]
        [InlineData(false, typeof(Stream))]
        public async Task StartAnalyze_Throws_On400(bool isAsync, Type payloadType)
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.BadRequest);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);


            // Act / Assert
            RequestFailedException ex;
            if (payloadType == typeof(Stream))
            {
                var requestStream = new MemoryStream();
                var requestType = FormContentType.Pdf;
                ex = isAsync
                    ? await Assert.ThrowsAsync<RequestFailedException>(() => client.StartAnalyzeAsync(requestStream, requestType))
                    : Assert.Throws<RequestFailedException>(() => client.StartAnalyze(requestStream, requestType));
            }
            else if (payloadType == typeof(Uri))
            {
                var requestUri = new Uri("http://localhost/fake/file");
                ex = isAsync
                    ? await Assert.ThrowsAsync<RequestFailedException>(() => client.StartAnalyzeAsync(requestUri))
                    : Assert.Throws<RequestFailedException>(() => client.StartAnalyze(requestUri));
            }
            else
            {
                throw new NotSupportedException("Test payload type is not supported");
            }
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Fact]
        public void StartAnalyze_Returns_Operation()
        {
            // Arrange
            var operationId = "fake-operation-id";
            var client = GetClient();

            // Act
            var operation = client.StartAnalyze(operationId);

            // Assert
            Assert.NotNull(operation);
            Assert.Equal(operationId, operation.Id);
        }

        private AnalyzeClient<AnalyzeOptions> GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new AnalyzeClient<AnalyzeOptions>(pipeline, options, "/fake-path");
        }
    }
}