// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Http;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Http
{
    public class FormHttpPolicyTests
    {
        private const string EndpointText = "http://localhost";
        private const string ApiKey = "fake-key";


        private readonly Uri _endpoint;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;


        public FormHttpPolicyTests()
        {
            _endpoint = new Uri(EndpointText);
            _options = new FormRecognizerClientOptions();
            var transport = new MockTransport(new MockResponse(200));
            var credential = new CognitiveKeyCredential(ApiKey);
            var policy = new FormHttpPolicy(_endpoint, new FormAuthenticator(credential), _options.Version);
            _pipeline = new HttpPipeline(transport, new[] { policy });
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
            Assert.Equal($"/formrecognizer/{FormHttpPolicy.GetVersionString(_options.Version)}/foo?x=1", request.Uri.PathAndQuery);
        }
    }
}