// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.Core;
using Xunit;

// TestFramework:
// https://github.com/Azure/azure-sdk-for-net/blob/e6de379e53add4f5296ba9d191f5062d58e1759a/doc/dev/Track2TestFramework.md

namespace Azure.AI.FormRecognizer.Tests
{
    public class FormRecognizerClientTests
    {
        [Fact]
        public void KeyConstructor_Throws_OnInvalidParams()
        {
            // Act / assert
            Assert.Throws<ArgumentNullException>(() => new CustomFormClient(null, (CognitiveApiKeyCredential)null));
            Assert.Throws<ArgumentNullException>(() => new CustomFormClient(null, (CognitiveApiKeyCredential)null, null));
        }

        [Fact]
        public void TokenConstructor_Throws_OnInvalidParams()
        {
            // Act / assert
            Assert.Throws<ArgumentNullException>(() => new CustomFormClient(null, (TokenCredential)null));
            Assert.Throws<ArgumentNullException>(() => new CustomFormClient(null, (TokenCredential)null, null));
        }
    }
}
