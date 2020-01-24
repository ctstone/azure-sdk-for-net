// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Http;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Http
{
    public class FormHttpPolicyTests
    {
        private const string Endopint = "http://localhost";
        private const string ApiKey = "fake-key";

        private readonly FormHttpPolicy _policy;
        private readonly Uri _endpoint;
        private readonly MockTransport _transport;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;


        public FormHttpPolicyTests()
        {
            _options = new FormRecognizerClientOptions();
            _transport = new MockTransport(new MockResponse(200));
            _endpoint = new Uri(Endopint);
            _policy = new FormHttpPolicy(_endpoint, ApiKey, _options);
            _pipeline = new HttpPipeline(_transport, new[] { _policy });
        }

        [Fact]
        public void Constructor_Sets_Parameters()
        {
            Assert.NotNull(_policy.ApiKey);
            Assert.NotNull(_policy.Endpoint);
            Assert.Equal(_endpoint, _policy.Endpoint);
            Assert.Equal(ApiKey, _policy.ApiKey);
        }

        [Fact]
        public void Parameters_AreSettable()
        {
            // Arrange
            var apiKey = "new-key";
            var endpoint = new Uri("http://localhost:5000");

            // Act
            _policy.ApiKey = apiKey;
            _policy.Endpoint = endpoint;

            // Assert
            Assert.Equal(endpoint, _policy.Endpoint);
            Assert.Equal(apiKey, _policy.ApiKey);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Process_SetsApiKeyHeader(bool isAsync)
        {
            // Arrange
            var request = _pipeline.CreateRequest();

            // Act
            if (isAsync)
            {
                await _pipeline.SendRequestAsync(request, CancellationToken.None);
            }
            else
            {
                _pipeline.SendRequest(request, CancellationToken.None);
            }

            // Assert
            string apiKeyHeader;
            var isValid = request.Headers.TryGetValue("Ocp-Apim-Subscription-Key", out apiKeyHeader);
            Assert.True(isValid);
            Assert.Equal(ApiKey, apiKeyHeader);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Process_SetsClientRequestId(bool isAsync)
        {
            // Arrange
            var request = _pipeline.CreateRequest();

            // Act
            if (isAsync)
            {
                await _pipeline.SendRequestAsync(request, CancellationToken.None);
            }
            else
            {
                _pipeline.SendRequest(request, CancellationToken.None);
            }

            // Assert
            string clientRequestId;
            var isValid = request.Headers.TryGetValue("client-request-id", out clientRequestId);
            Assert.True(isValid);
            Assert.NotNull(clientRequestId);
        }

        [Theory]
        [InlineData(true, "/foo")]
        [InlineData(false, "/foo")]
        [InlineData(true, "foo")]
        [InlineData(false, "foo")]
        public async Task Process_SetsRequestUri(bool isAsync, string testPath)
        {
            // Arrange
            var request = _pipeline.CreateRequest();
            request.Uri.Path = testPath;
            request.Uri.AppendQuery("x", "1");

            // Act
            if (isAsync)
            {
                await _pipeline.SendRequestAsync(request, CancellationToken.None);
            }
            else
            {
                _pipeline.SendRequest(request, CancellationToken.None);
            }

            // Assert
            Assert.Equal(_endpoint.Scheme, request.Uri.Scheme);
            Assert.Equal(_endpoint.Host, request.Uri.Host);
            Assert.Equal(_endpoint.Port, request.Uri.Port);
            Assert.Equal($"/formrecognizer/{_options.GetVersionString()}/foo?x=1", request.Uri.PathAndQuery);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task Process_SetsExtraHeaders(bool isAsync)
        {
            // Arrange
            var request = _pipeline.CreateRequest();
            _options.ExtraHeaders.Add(new HttpHeader("Foo", "Bar"));

            // Act
            if (isAsync)
            {
                await _pipeline.SendRequestAsync(request, CancellationToken.None);
            }
            else
            {
                _pipeline.SendRequest(request, CancellationToken.None);
            }

            // Assert
            string customHeader;
            var isValid = request.Headers.TryGetValue("Foo", out customHeader);
            Assert.True(isValid);
            Assert.Equal("Bar", customHeader);
        }
    }
}