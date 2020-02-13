// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests
{
    public class CognitiveKeyCredentialTests
    {
        [Fact]
        public void Refresh_Updates_Credential()
        {
            // Arrange
            var key1 = "fake-key-1";
            var key2 = "fake-key-2";
            var pipeline = new HttpPipeline(new MockTransport());
            var request = pipeline.CreateRequest();
            var credential = new CognitiveApiKeyCredential(key1);

            // Act
            credential.UpdateCredential(key2);
            credential.Authenticate(request);

            // Assert
            AssertHasKeyHeader(key2, request);
        }

        [Fact]
        public void Authorize_Sets_HeaderKey()
        {
            // Arrange
            var key = "fake-key";
            var credential = new CognitiveApiKeyCredential(key);
            var pipeline = new HttpPipeline(new MockTransport());
            var request = pipeline.CreateRequest();

            // Act
            credential.Authenticate(request);

            // Assert
            AssertHasKeyHeader(key, request);
        }

        [Fact]
        public async Task Authorize_Sets_HeaderKeyAsync()
        {
            // Arrange
            var key = "fake-key";
            var credential = new CognitiveApiKeyCredential(key);
            var pipeline = new HttpPipeline(new MockTransport());
            var request = pipeline.CreateRequest();

            // Act
            await credential.AuthenticateAsync(request);

            // Assert
            AssertHasKeyHeader(key, request);
        }

        private void AssertHasKeyHeader(string expectValue, Request request)
        {
            var hasHeader = request.Headers.TryGetValue("ocp-apim-subscription-key", out string headerValue);
            Assert.True(hasHeader);
            Assert.Equal(expectValue, headerValue);
        }
    }
}