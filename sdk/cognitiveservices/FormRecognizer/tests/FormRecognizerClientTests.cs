// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests
{
    public class FormRecognizerClientTests
    {
        [Fact]
        public void Constructor_Throws_OnInvalidParams()
        {
            var validEndpoint = new Uri("http://localhost");
            var invalidEndpointNotAbsolute = new Uri("/");
            var validKey = "fake-key";
            var invalidKeyEmpty = String.Empty;
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(default, default));
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(default, default, default));
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(validEndpoint, default));
            Assert.Throws<ArgumentNullException>(() => new FormRecognizerClient(default, validKey));
            Assert.Throws<ArgumentException>(() => new FormRecognizerClient(validEndpoint, invalidKeyEmpty));
            Assert.Throws<ArgumentException>(() => new FormRecognizerClient(invalidEndpointNotAbsolute, validKey));
        }
    }
}
