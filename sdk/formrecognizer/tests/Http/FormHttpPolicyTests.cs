// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Http;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Http
{
    public class FormHttpPolicyTests
    {
        private const string Endpoint = "http://localhost";
        private const string ApiKey = "fake-key";

        private readonly FormHttpPolicy _policy;
        private readonly CognitiveCredential _credential;
        private readonly MockTransport _transport;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;


        public FormHttpPolicyTests()
        {
            var endpoint = new Uri(Endpoint);
            _options = new FormRecognizerClientOptions();
            _transport = new MockTransport(new MockResponse(200));
            _credential = new CognitiveCredential(endpoint, ApiKey);
            _policy = new FormHttpPolicy(_credential, _options.Version);
            _pipeline = new HttpPipeline(_transport, new[] { _policy });
        }

        [Fact]
        public void Constructor_Sets_Parameters()
        {
            Assert.NotNull(_policy.Credential.Endpoint);
            Assert.Equal(_credential, _policy.Credential);
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
            Assert.Equal(_credential.Endpoint.Scheme, request.Uri.Scheme);
            Assert.Equal(_credential.Endpoint.Host, request.Uri.Host);
            Assert.Equal(_credential.Endpoint.Port, request.Uri.Port);
            Assert.Equal($"/formrecognizer/{FormHttpPolicy.GetVersionString(_options.Version)}/foo?x=1", request.Uri.PathAndQuery);
        }
    }
}