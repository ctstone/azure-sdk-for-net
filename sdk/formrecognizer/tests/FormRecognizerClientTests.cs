// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Xunit;

// TestFramework:
// https://github.com/Azure/azure-sdk-for-net/blob/e6de379e53add4f5296ba9d191f5062d58e1759a/doc/dev/Track2TestFramework.md

namespace Azure.AI.FormRecognizer.Tests
{
    public class FormRecognizerClientTests
    {
        [Fact]
        public void Constructor_Throws_OnInvalidParams()
        {
            // Arrange
            var validEndpoint = new Uri("http://localhost");
            var invalidEndpointNotAbsolute = new Uri("/");
            var validKey = "fake-key";
            var invalidKeyEmpty = String.Empty;

            // Act / assert
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(default, default));
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(default, default, default));
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(validEndpoint, default));
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(default, validKey));
            Assert.Throws<ArgumentException>(() => new FormRecognizerClient(validEndpoint, invalidKeyEmpty));
            Assert.Throws<ArgumentException>(() => new FormRecognizerClient(invalidEndpointNotAbsolute, validKey));
        }

        [Fact]
        public void Constructor_Creates_Properties()
        {
            // Act
            var apiKey = "fake-key";
            var client = new FormRecognizerClient(new Uri("http://localhost"), apiKey);

            // Assert
            Assert.NotNull(client.Custom);
            Assert.NotNull(client.Prebuilt);
            Assert.NotNull(client.Layout);
            Assert.Equal(apiKey, client.ApiKey);
        }

        [Fact]
        public void Client_Updates_ApiKey()
        {
            // Act
            var apiKey = "fake-key-1";
            var updatedApiKey = "fake-key-2";
            var client = new FormRecognizerClient(new Uri("http://localhost"), apiKey);
            client.ApiKey = updatedApiKey;

            // Assert
            Assert.Equal(updatedApiKey, client.ApiKey);
        }

        [Fact]
        public void Client_Updates_Endpoint()
        {
            // Act
            var endpoint = new Uri("http://localhost/");
            var updatedEndpoint = new Uri("http://localhost:5000/");
            var client = new FormRecognizerClient(endpoint, "fake-key");
            client.Endpoint = updatedEndpoint;

            // Assert
            Assert.Equal(updatedEndpoint, client.Endpoint);
        }
    }
}
