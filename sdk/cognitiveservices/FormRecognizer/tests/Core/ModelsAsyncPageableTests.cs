// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class ModelsAsyncPageableTests
    {
        [Theory(Timeout = 1000)]
        [InlineData("")]
        [InlineData(null)]
        public async Task AsPages_ReturnsAllPages_On200(string finalNextLink)
        {
            // Arrange
            var responses = new[] {
                @"{ ""modelList"": [
                    { ""modelInfo"": { ""modelId"": ""1"" } },
                    { ""modelInfo"": { ""modelId"": ""2"" } } ],
                    ""nextLink"": ""http://localhost/fake-next-page""}",
                @"{ ""modelList"": [
                    { ""modelInfo"": { ""modelId"": ""3"" } },
                    { ""modelInfo"": { ""modelId"": ""4"" } },
                    { ""modelInfo"": { ""modelId"": ""5"" } } ],
                    ""nextLink"": {finalNextLink} }".Replace("{finalNextLink}", finalNextLink == null ? "null" : $"\"{finalNextLink}\""),
            }.Select((content) =>
            {
                var mockResponse = new MockResponse((int)HttpStatusCode.OK);
                mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
                mockResponse.SetContent(content);
                return mockResponse;
            }).ToArray();
            var client = GetClient(responses);
            var pageCount = 0;
            var modelCount = 0;

            // Act
            var pages = client.AsPages();
            await foreach (var page in pages)
            {
                pageCount += 1;
                modelCount += page.Values.Count;
            }

            // Assert
            Assert.Equal(responses.Length, pageCount);
            Assert.Equal(5, modelCount);
        }

        [Fact]
        public async Task AsPages_Throws_On404()
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.NotFound);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = await Assert.ThrowsAsync<RequestFailedException>(async () => await client.AsPages().GetAsyncEnumerator().MoveNextAsync());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Fact]
        public async Task GetPage_ReturnsPage_On200()
        {
            // Arrange
            var continuationToken = "http://localhost/fake-next-page";
            var content = @"{ ""modelList"": [
                { ""modelInfo"": { ""modelId"": ""1"" } },
                { ""modelInfo"": { ""modelId"": ""2"" } } ],
                ""nextLink"": ""{continuationToken}""}".Replace("{continuationToken}", continuationToken);
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act
            var page = await client.GetPageAsync();

            // Assert
            Assert.NotNull(page);
            Assert.NotNull(page.Values);
            Assert.Equal(2, page.Values.Count);
            Assert.NotNull(page.ContinuationToken);
            Assert.Equal(continuationToken, page.ContinuationToken);
        }

        [Fact]
        public async Task GetPage_Throws_On200()
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.NotFound);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = await Assert.ThrowsAsync<RequestFailedException>(async () => await client.GetPageAsync());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Theory(Timeout = 1000)]
        [InlineData("")]
        [InlineData(null)]
        public async Task GetAsyncEnumerator_ReturnsAllModels_On200(string finalNextLink)
        {
            // Arrange
            var responses = new[] {
                @"{ ""modelList"": [
                    { ""modelInfo"": { ""modelId"": ""1"" } },
                    { ""modelInfo"": { ""modelId"": ""2"" } } ],
                    ""nextLink"": ""http://localhost/fake-next-page""}",
                @"{ ""modelList"": [
                    { ""modelInfo"": { ""modelId"": ""3"" } },
                    { ""modelInfo"": { ""modelId"": ""4"" } },
                    { ""modelInfo"": { ""modelId"": ""5"" } } ],
                    ""nextLink"": {finalNextLink} }".Replace("{finalNextLink}", finalNextLink == null ? "null" : $"\"{finalNextLink}\""),
            }.Select((content) =>
            {
                var mockResponse = new MockResponse((int)HttpStatusCode.OK);
                mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
                mockResponse.SetContent(content);
                return mockResponse;
            }).ToArray();
            var client = GetClient(responses);
            var modelCount = 0;

            // Act
            await foreach (var modelInfo in client)
            {
                modelCount += 1;
            }

            // Assert
            Assert.Equal(5, modelCount);
        }

        [Fact]
        public async Task GetAsyncEnumerator_Throws_On404()
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.NotFound);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = await Assert.ThrowsAsync<RequestFailedException>(async () => await client.GetAsyncEnumerator().MoveNextAsync());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        private ModelsAsyncPageable GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new ModelsAsyncPageable(pipeline, options, CancellationToken.None);
        }
    }
}