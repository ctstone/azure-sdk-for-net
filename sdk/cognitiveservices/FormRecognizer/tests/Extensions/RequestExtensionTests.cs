// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
using Azure.AI.FormRecognizer.Extensions;

namespace Azure.AI.FormRecognizer.Tests.Extensions
{
    public class RequestExtensionTests
    {
        private readonly MockTransport _mockTransport;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;

        public RequestExtensionTests()
        {
            _mockTransport = new MockTransport();
            _pipeline = new HttpPipeline(_mockTransport);
            _options = new FormRecognizerClientOptions();
        }

        [Fact]
        public void AddJsonContent_UpdatesRequest()
        {
            // Arrange
            var request = new MockRequest();
            var obj = new { Foo = "Bar" };

            // Act
            request.AddJsonContent(obj, _options);

            // Assert
            long requestSize;
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.True(requestSize > 0);
            Assert.Equal("application/json", requestContentType.Value);
        }

        [Fact]
        public async Task AddJsonContentAsync_UpdatesRequest()
        {
            // Arrange
            var request = new MockRequest();
            var obj = new { Foo = "Bar" };

            // Act
            await request.AddJsonContentAsync(obj, _options, CancellationToken.None);

            // Assert
            long requestSize;
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.True(requestSize > 0);
            Assert.Equal("application/json", requestContentType.Value);
        }

        [Theory]
        [InlineData(FormContentType.Pdf, "application/pdf")]
        [InlineData(FormContentType.Png, "image/png")]
        [InlineData(FormContentType.Jpeg, "image/jpeg")]
        [InlineData(FormContentType.Tiff, "image/tiff")]
        public void AddBinaryContent_UpdatesRequest(FormContentType testContentType, string expectContentTypeHeader)
        {
            // Arrange
            var request = new MockRequest();
            var stream = new MemoryStream(new byte[] { 1, 2, 3 });

            // Act
            request.AddBinaryContent(testContentType, stream);

            // Assert
            long requestSize;
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.True(requestSize == stream.Length);
            Assert.Equal(expectContentTypeHeader, requestContentType.Value);
        }
    }
}