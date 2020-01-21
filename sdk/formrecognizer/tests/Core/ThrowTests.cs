// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Tests.TestUtilities;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class ThrowTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData("123", true)]
        [InlineData(null, false)]
        public void IfMissing_Throws_WhenNull(string value, bool isValid)
        {
            if (isValid)
            {
                Throw.IfMissing(value, "foo");
            }
            else
            {
                var ex = Assert.Throws<ArgumentNullException>(() => Throw.IfMissing(value, "foo"));
                Assert.Equal("foo", ex.ParamName);
            }
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("123", true)]
        [InlineData(null, false)]
        public void IfNullOrEmpty_Throws_WhenNullOrEmpty(string value, bool isValid)
        {
            if (isValid)
            {
                Throw.IfNullOrEmpty(value, "foo");
            }
            else
            {
                var ex = Assert.Throws<ArgumentException>(() => Throw.IfNullOrEmpty(value, "foo"));
                Assert.Equal("foo", ex.ParamName);
            }
        }

        [Theory]
        [InlineData("http://localhost", true)]
        [InlineData("http://localhost/path", true)]
        [InlineData("http://localhost:1234/path", true)]
        [InlineData("https://localhost:1234/path", true)]
        [InlineData("//localhost", false)]
        [InlineData("localhost", false)]
        [InlineData("file:///foo.txt", false)]
        public void IfInvalidUri_Throws_ForInvalidUri(string uriText, bool isValid)
        {
            var uri = new Uri(uriText, UriKind.RelativeOrAbsolute);
            if (isValid)
            {
                Throw.IfInvalidUri(uri, "foo");
            }
            else
            {
                var ex = Assert.Throws<ArgumentException>(() => Throw.IfInvalidUri(uri, "foo"));
                Assert.Equal("foo", ex.ParamName);
            }
        }
    }
}