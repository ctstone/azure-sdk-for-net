// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Custom;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Custom
{
    public class CustomFormModelClientTests
    {
        private const string ModelId = "fake-model-id";

        [Fact]
        public void Constructor_Throws_OnMissingParams()
        {
            var validModelId = "fake-model-id";
            var validPipeline = new HttpPipeline(new MockTransport());
            var validOptions = new FormClientOptions();
            var ex1 = Assert.Throws<ArgumentException>(() => new FormModelReference(null, validPipeline, validOptions.SerializationOptions));
            var ex2 = Assert.Throws<ArgumentException>(() => new FormModelReference(String.Empty, validPipeline, validOptions.SerializationOptions));
            var ex3 = Assert.Throws<ArgumentNullException>(() => new FormModelReference(validModelId, null, validOptions.SerializationOptions));
            var ex4 = Assert.Throws<ArgumentNullException>(() => new FormModelReference(validModelId, validPipeline, null));

            Assert.NotNull(ex1.ParamName);
            Assert.NotNull(ex2.ParamName);
            Assert.NotNull(ex3.ParamName);
            Assert.NotNull(ex4.ParamName);
        }

        [Fact]
        public void Class_Is_AnalyzeClient()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.True(client is AnalyzeClient<FormAnalysis>);
        }

        [Fact]
        public void Constructor_Sets_ModelId()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.Equal(ModelId, client.ModelId);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Get_ReturnsModel_On200(bool isAsync)
        {
            // Arrange
            var content = @"{ ""modelInfo"": { ""modelId"": ""{modelId}"" } }".Replace("{modelId}", ModelId);
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act
            var model = isAsync
                ? await client.GetAsync()
                : client.Get();

            // Assert
            Assert.NotNull(model);
            Assert.NotNull(model.Value);
            Assert.NotNull(model.Value.Information);
            Assert.Equal(ModelId, model.Value.Information.Id);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Get_Throws_On500(bool isAsync)
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.InternalServerError);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<RequestFailedException>(() => client.GetAsync())
                : Assert.Throws<RequestFailedException>(() => client.Get());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        private FormModelReference GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormClientOptions();
            return new FormModelReference(ModelId, pipeline, options.SerializationOptions);
        }
    }
}