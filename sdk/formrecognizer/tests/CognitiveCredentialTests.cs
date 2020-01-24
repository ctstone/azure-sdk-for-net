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
    public class CognitiveCredentialTests
    {
        public CognitiveCredentialTests()
        {
        }

        [Fact]
        public void Constructor_Sets_SubscriptionKey()
        {
            // Arrange
            var endpoint = (Uri)CognitiveEndpoint.EastUnitedStates;
            var key = "fake-key";

            // Act
            var credential = new CognitiveCredential(endpoint, key);

            // Assert
            Assert.Equal(endpoint, credential.Endpoint);
            var keyHeader = credential.Headers.FirstOrDefault((x) => x.Name.Equals("Ocp-Apim-Subscription-Key", StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(keyHeader.Value);
            Assert.Equal(key, keyHeader.Value);
        }

        [Fact]
        public void Refresh_Updates_Credential()
        {
            // Arrange
            var endpoint1 = CognitiveEndpoint.EastUnitedStates;
            var key1 = "fake-key-1";
            var endpoint2 = CognitiveEndpoint.WestUnitedStates2;
            var key2 = "fake-key-2";
            var credential = new CognitiveCredential(endpoint1, key1);

            // Act
            credential.Refresh(endpoint2, key2);

            // Assert
            Assert.Equal((Uri)endpoint2, credential.Endpoint);
            var keyHeader = credential.Headers.FirstOrDefault((x) => x.Name.Equals("Ocp-Apim-Subscription-Key", StringComparison.OrdinalIgnoreCase));
            Assert.NotNull(keyHeader.Value);
            Assert.Equal(key2, keyHeader.Value);
        }

        [Fact]
        public void Authorize_Sets_HeaderKey()
        {
            // Arrange
            var endpoint = (Uri)CognitiveEndpoint.EastUnitedStates;
            var key = "fake-key";
            var credential = new CognitiveCredential(endpoint, key);
            var pipeline = new HttpPipeline(new MockTransport());
            var request = pipeline.CreateRequest();

            // Act
            credential.Authorize(request);

            // Assert
            var hasHeader = request.Headers.TryGetValue("ocp-apim-subscription-key", out string headerValue);
            Assert.True(hasHeader);
            Assert.Equal(key, headerValue);
        }

        [Fact]
        public async Task Authorize_Sets_HeaderKeyAsync()
        {
            // Arrange
            var endpoint = (Uri)CognitiveEndpoint.EastUnitedStates;
            var key = "fake-key";
            var credential = new CognitiveCredential(endpoint, key);
            var pipeline = new HttpPipeline(new MockTransport());
            var request = pipeline.CreateRequest();

            // Act
            await credential.AuthorizeAsync(request, default);

            // Assert
            var hasHeader = request.Headers.TryGetValue("ocp-apim-subscription-key", out string headerValue);
            Assert.True(hasHeader);
            Assert.Equal(key, headerValue);
        }
    }
}