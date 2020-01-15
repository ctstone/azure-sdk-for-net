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
            var client = new FormRecognizerClient(new Uri("http://localhost"), "fake-key");

            // Assert
            Assert.NotNull(client.Custom);
            Assert.NotNull(client.Prebuilt);
            Assert.NotNull(client.Layout);
            Assert.Equal(apiKey, client.ApiKey);
        }
    }
}
