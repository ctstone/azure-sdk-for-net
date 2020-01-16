// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CustomFormModelClientTests
    {
        private const string ModelId = "fake-model-id";

        [Fact]
        public void Constructor_Throws_OnMissingParams()
        {
            var validModelId = "fake-model-id";
            var validPipeline = new HttpPipeline(new MockTransport());
            var validOptions = new FormRecognizerClientOptions();
            var ex1 = Assert.Throws<ArgumentException>(() => new CustomFormModelClient(null, validPipeline, validOptions));
            var ex2 = Assert.Throws<ArgumentException>(() => new CustomFormModelClient(String.Empty, validPipeline, validOptions));
            var ex3 = Assert.Throws<ArgumentNullException>(() => new CustomFormModelClient(validModelId, null, validOptions));
            var ex4 = Assert.Throws<ArgumentNullException>(() => new CustomFormModelClient(validModelId, validPipeline, null));

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
            Assert.True(client is AnalyzeClient<AnalyzeOptions>);
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
            Assert.NotNull(model.Value.ModelInfo);
            Assert.Equal(ModelId, model.Value.ModelInfo.ModelId);
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

        private CustomFormModelClient GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new CustomFormModelClient(ModelId, pipeline, options);
        }
    }
}