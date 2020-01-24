// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests
{
    public class CognitiveEndpointTests
    {
        [Fact]
        public void Endpoint_Is_ImplicitUri()
        {
            // Act
            var endpoint = CognitiveEndpoint.EastUnitedStates;
            Uri uri = endpoint;
        }

        [Fact]
        public void Uri_Is_ImplicitEndpoint()
        {
            // Act
            var uri = new Uri("http://localhost");
            CognitiveEndpoint endpoint = uri;
        }

        // TOOD: make this a theory with all static endpoints.
        [Fact]
        public void ToString_Returns_Uri()
        {
            // Act
            var endpoint = CognitiveEndpoint.EastUnitedStates;

            // Assert
            Assert.Equal("https://eastus.api.cognitive.microsoft.com/", endpoint.ToString());
        }

        [Fact]
        public void Equals_Compares_Uri()
        {
            // Act
            var endpoint1 = CognitiveEndpoint.EastUnitedStates;
            var endpoint2 = new CognitiveEndpoint(new Uri("https://eastus.api.cognitive.microsoft.com/"));

            // Assert
            Assert.Equal(endpoint1, endpoint2);
            Assert.True(endpoint1.Equals(endpoint2));
            Assert.True(endpoint1 == endpoint2);
        }

        [Fact]
        public void NotEquals_Compares_Uri()
        {
            // Act
            var endpoint1 = CognitiveEndpoint.EastUnitedStates;
            var endpoint2 = CognitiveEndpoint.WestUnitedStates2;

            // Assert
            Assert.False(endpoint1.Equals(endpoint2));
            Assert.True(endpoint1 != endpoint2);
        }
    }
}