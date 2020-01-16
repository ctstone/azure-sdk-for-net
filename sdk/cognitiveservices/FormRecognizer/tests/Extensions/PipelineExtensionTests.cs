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
    public class PipelineExtensionTests
    {
        private const string FakeOptionQuery = "fake-option";

        private readonly MockTransport _mockTransport;
        private readonly HttpPipeline _pipeline;

        public PipelineExtensionTests()
        {
            _mockTransport = new MockTransport();
            _pipeline = new HttpPipeline(_mockTransport);
        }

        [Fact]
        public void CreateAnalyzeStreamRequest_CreatesRequest()
        {
            // Arrange
            var basePath = "/fake/base/path";
            var stream = new MemoryStream(new byte[] { 1, 2, 3 });
            var contentType = FormContentType.Pdf;
            var options = new MockOptions { Foo = "foo" };


            // Act
            var request = _pipeline.CreateAnalyzeStreamRequest<MockOptions>(basePath, stream, contentType, options, SetOption);

            // Assert
            Assert.Equal(RequestMethod.Post, request.Method);
            Assert.Equal($"{basePath}/analyze?fake-option=foo", request.Uri.PathAndQuery);
            long requestSize;
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.Equal(stream.Length, requestSize);
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.Equal("application/pdf", requestContentType.Value);
        }

        private static void SetOption(MockOptions options, Request request)
        {
            request.Uri.AppendQuery(FakeOptionQuery, options.Foo);
        }
    }

    public struct MockOptions
    {
        public string Foo { get; set; }
    }
}