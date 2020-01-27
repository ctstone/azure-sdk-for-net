// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using System.Net;
using System.Threading;
using Azure.AI.FormRecognizer.Custom;
using Azure.AI.FormRecognizer.Tests.TestUtilities;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Custom
{
    public class ModelsPageableTests
    {
        [Theory(Timeout = 1000)]
        [InlineData("")]
        [InlineData(null)]
        public void AsPages_ReturnsAllPages_On200(string finalNextLink)
        {
            // Arrange
            var pagesInfo = new[]
                {
                    new { models = 3, nextLink = "http://localhost/fake-next"},
                    new { models = 4, nextLink = finalNextLink },
                };
            var responses = pagesInfo
                .Select((x) => MockFormResponses.GetPageResponse(x.models, x.nextLink))
                .ToArray();
            var client = GetClient(responses);
            var totalPages = pagesInfo.Length;
            var totalModels = pagesInfo.Sum((x) => x.models);
            var pageCount = 0;
            var modelCount = 0;

            // Act
            var pages = client.AsPages();
            foreach (var page in pages)
            {
                pageCount += 1;
                modelCount += page.Values.Count;
            }

            // Assert
            Assert.Equal(totalPages, pageCount);
            Assert.Equal(totalModels, modelCount);
        }

        [Fact]
        public void AsPages_Throws_On404()
        {
            // Arrange
            var mockResponse = MockFormResponses.GetErrorResponse(HttpStatusCode.NotFound, "123", "foo");
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = Assert.Throws<RequestFailedException>(() => client.AsPages().GetEnumerator().MoveNext());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Fact]
        public void GetPage_ReturnsPage_On200()
        {
            // Arrange
            var continuationToken = "http://localhost/fake-next-page";
            var mockResponse = MockFormResponses.GetPageResponse(2, continuationToken);
            var client = GetClient(mockResponse);

            // Act
            var page = client.GetPage();

            // Assert
            Assert.NotNull(page);
            Assert.NotNull(page.Values);
            Assert.Equal(2, page.Values.Count);
            Assert.NotNull(page.ContinuationToken);
            Assert.Equal(continuationToken, page.ContinuationToken);
        }

        [Fact]
        public void GetPage_Throws_On200()
        {
            // Arrange
            var mockResponse = MockFormResponses.GetErrorResponse(HttpStatusCode.NotFound, "123", "foo");
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = Assert.Throws<RequestFailedException>(() => client.GetPage());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Theory(Timeout = 1000)]
        [InlineData("")]
        [InlineData(null)]
        public void GetAsyncEnumerator_ReturnsAllModels_On200(string finalNextLink)
        {
            // Arrange
            var pagesInfo = new[]
                {
                    new { models = 3, nextLink = "http://localhost/fake-next"},
                    new { models = 4, nextLink = finalNextLink },
                };
            var responses = pagesInfo
                .Select((x) => MockFormResponses.GetPageResponse(x.models, x.nextLink))
                .ToArray();
            var client = GetClient(responses);
            var totalModels = pagesInfo.Sum((x) => x.models);
            var modelCount = 0;

            // Act
            foreach (var modelInfo in client)
            {
                modelCount += 1;
            }

            // Assert
            Assert.Equal(totalModels, modelCount);
        }

        [Fact]
        public void GetAsyncEnumerator_Throws_On404()
        {
            // Arrange
            var mockResponse = MockFormResponses.GetErrorResponse(HttpStatusCode.NotFound, "123", "foo");
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = Assert.Throws<RequestFailedException>(() => client.GetEnumerator().MoveNext());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        private ModelsPageable GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new ModelsPageable(pipeline, options, CancellationToken.None);
        }
    }
}