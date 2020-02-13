// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.TestUtilities
{
    public class LanguageTests
    {
        [Fact]
        public void Language_Supports_ArbitraryValues()
        {
            Language language = "foo";

            Assert.Equal("foo", language);
        }
    }
}