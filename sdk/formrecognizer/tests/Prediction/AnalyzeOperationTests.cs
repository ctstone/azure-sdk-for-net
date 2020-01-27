// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Prediction
{
    public class AnalyzeOperationTests
    {
        private const string FakeBasePath = "/fake-path";
        private const string FakeOperationId = "123";

        [Fact]
        public void Operation_Initializes()
        {
            var op = GetOperation();
            Assert.Equal(FakeOperationId, op.Id);
            Assert.False(op.HasCompleted);
            Assert.False(op.HasValue);
            Assert.Null(op.Value);
            Assert.Null(op.GetRawResponse());
        }

        [Theory]
        [InlineData(true, "running", false, false)]
        [InlineData(false, "running", false, false)]
        [InlineData(true, "succeeded", true, true)]
        [InlineData(false, "succeeded", true, true)]
        [InlineData(true, "notStarted", false, false)]
        [InlineData(false, "notStarted", false, false)]
        [InlineData(true, "failed", true, false)]
        [InlineData(false, "failed", true, false)]
        public async Task UpdateStatus_Completes_On200(bool isAsync, string testStatus, bool expectCompleted, bool expectValue)
        {
            // Arrange
            var content = @"{ ""status"": ""{testStatus}"" }".Replace("{testStatus}", testStatus);
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var op = GetOperation(mockResponse);

            // Act
            var response = isAsync
                ? await op.UpdateStatusAsync()
                : op.UpdateStatus();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(response, op.GetRawResponse());
            Assert.Equal(expectCompleted, op.HasCompleted);
            Assert.Equal(expectValue, op.HasValue);
            if (expectValue)
            {
                Assert.NotNull(op.Value);
            }
            else
            {
                Assert.Null(op.Value);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdateStatus_Throws_On400(bool isAsync)
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.BadRequest);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var op = GetOperation(mockResponse);

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<RequestFailedException>(async () => await op.UpdateStatusAsync())
                : Assert.Throws<RequestFailedException>(() => op.UpdateStatus());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Theory(Timeout = 2000)]
        [InlineData("succeeded", OperationStatus.Succeeded)]
        [InlineData("failed", OperationStatus.Failed)]
        public async Task WaitForCompletion_ReturnsAnalysis_On200(string finalStatus, OperationStatus expectStatus)
        {
            // Arrange
            var responses = new[] {
                @"{ ""status"": ""notStarted"" }",
                @"{ ""status"": ""running"" }",
                @"{ ""status"": ""{finalStatus}"" }".Replace("{finalStatus}", finalStatus),
            }.Select((content) =>
            {
                var mockResponse = new MockResponse((int)HttpStatusCode.OK);
                mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
                mockResponse.SetContent(content);
                return mockResponse;
            }).ToArray();
            var op = GetOperation(responses);

            // Act
            var response = await op.WaitForCompletionAsync(TimeSpan.FromSeconds(0.1));

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.Equal(expectStatus, response.Value.Status);
        }

        private AnalyzeOperation GetOperation(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new AnalyzeOperation(pipeline, FakeBasePath, FakeOperationId, options);
        }
    }
}