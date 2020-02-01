// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Linq;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;
using Azure.AI.FormRecognizer.Extensions;

namespace Azure.AI.FormRecognizer.Tests.Extensions
{
    public class PipelineExtensionTests
    {
        private readonly MockTransport _mockTransport;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;

        public PipelineExtensionTests()
        {
            _mockTransport = new MockTransport();
            _pipeline = new HttpPipeline(_mockTransport);
            _options = new FormRecognizerClientOptions();
        }

        [Fact]
        public void CreateTrainRequest_CreatesRequest()
        {
            // Arrange
            var trainRequest = new TrainingRequest { Source = "http://localhost" };

            // Act
            var request = _pipeline.CreateTrainRequest(trainRequest, _options.SerializationOptions);

            // Assert
            long requestSize;
            Assert.Equal(RequestMethod.Post, request.Method);
            Assert.Equal($"/custom/models", request.Uri.PathAndQuery);
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.True(requestSize > 0);
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.Equal("application/json", requestContentType.Value);
        }

        [Fact]
        public void CreateGetModelRequest_CreatesRequest()
        {
            // Arrange
            var modelId = "fake-model-id";

            // Act
            var request = _pipeline.CreateGetModelRequest(modelId, null);

            // Assert
            Assert.Equal(RequestMethod.Get, request.Method);
            Assert.Equal($"/custom/models/{modelId}", request.Uri.PathAndQuery);
            Assert.Null(request.Content);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("summary")]
        [InlineData("full")]
        public void CreateListModelsRequest_CreatesRequest(string op)
        {
            // Arrange
            var expectQuery = op == default ? String.Empty : $"?op={op}";

            // Act
            var request = _pipeline.CreateListModelsRequest(op: op);

            // Assert
            Assert.Equal(RequestMethod.Get, request.Method);
            Assert.Equal($"/custom/models{expectQuery}", request.Uri.PathAndQuery);
            Assert.Null(request.Content);
        }

        [Fact]
        public void CreateDeleteModelRequest_CreatesRequest()
        {
            // Arrange
            var modelId = "fake-model-id";

            // Act
            var request = _pipeline.CreateDeleteModelRequest(modelId);

            // Assert
            Assert.Equal(RequestMethod.Delete, request.Method);
            Assert.Equal($"/custom/models/{modelId}", request.Uri.PathAndQuery);
            Assert.Null(request.Content);
        }

        [Fact]
        public void CreateGetAnalysisRequest_CreatesRequest()
        {
            // Arrange
            var basePath = "/fake/base/path";
            var operationId = "fake-id";

            // Act
            var request = _pipeline.CreateGetAnalysisRequest(basePath, operationId);

            // Assert
            Assert.Equal(RequestMethod.Get, request.Method);
            Assert.Equal($"{basePath}/analyzeResults/{operationId}", request.Uri.PathAndQuery);
            Assert.Null(request.Content);
        }

        [Fact]
        public void CreateAnalyzeStreamRequest_CreatesRequest()
        {
            // Arrange
            var basePath = "/fake/base/path";
            var stream = new MemoryStream(new byte[] { 1, 2, 3 });
            var contentType = FormContentType.Pdf;

            // Act
            var request = _pipeline.CreateAnalyzeStreamRequest(basePath, stream, contentType, default);

            // Assert
            long requestSize;
            Assert.Equal(RequestMethod.Post, request.Method);
            Assert.Equal($"{basePath}/analyze", request.Uri.PathAndQuery);
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.Equal(stream.Length, requestSize);
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.Equal("application/pdf", requestContentType.Value);
        }

        [Fact]
        public void CreateAnalyzeUriRequest_CreatesRequest()
        {
            // Arrange
            var basePath = "/fake/base/path";
            var uri = new Uri("http://localhost/fake-file.pdf");

            // Act
            var request = _pipeline.CreateAnalyzeUriRequest(basePath, uri, default, _options.SerializationOptions);

            // Assert
            Assert.Equal(RequestMethod.Post, request.Method);
            Assert.Equal($"{basePath}/analyze", request.Uri.PathAndQuery);
            long requestSize;
            Assert.True(request.Content.TryComputeLength(out requestSize));
            Assert.True(requestSize > 0);
            var requestContentType = request.Headers.FirstOrDefault((x) => x.Name == "Content-Type");
            Assert.Equal("application/json", requestContentType.Value);
        }
    }
}