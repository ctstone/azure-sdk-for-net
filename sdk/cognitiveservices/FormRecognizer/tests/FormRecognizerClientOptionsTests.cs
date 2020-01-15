// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using Azure.Core;
using Xunit;
using static Azure.AI.FormRecognizer.FormRecognizerClientOptions;

namespace Azure.AI.FormRecognizer.Tests
{
    public class FormRecognizerClientOptionsTests
    {
        [Fact]
        public void Constructor_Handles_DefaultParameters()
        {
            // Act
            var options = new FormRecognizerClientOptions();

            // Assert
            Assert.Equal(Encoding.UTF8, options.Encoding);
            Assert.Equal(FormRecognizerClientOptions.LatestVersion, options.Version);
            Assert.Null(options.UserAgent);
            Assert.Null(options.ExtraHeaders);
            Assert.NotNull(options.SerializationOptions);
            Assert.NotNull(options.GetVersionString());
        }

        [Fact]
        public void Constructor_Handles_VersionParameter()
        {
            // Arrange
            var version = ServiceVersion.V2_0_preview;
            var options = new FormRecognizerClientOptions(version: version);

            // Assert
            Assert.Equal(version, options.Version);
        }

        [Fact]
        public void Constructor_Handles_UserAgentParameter()
        {
            // Arrange
            var userAgent = "fake/agent";
            var options = new FormRecognizerClientOptions(userAgent: userAgent);

            // Assert
            Assert.Equal(userAgent, options.UserAgent);
        }

        [Fact]
        public void Constructor_Handles_ExtraHeadersParameter()
        {
            // Arrange
            var extraHeaders = new HttpHeader[]
            {
                new HttpHeader("foo", "bar"),
            };
            var options = new FormRecognizerClientOptions(extraHeaders: extraHeaders);

            // Assert
            Assert.Equal(extraHeaders, options.ExtraHeaders);
        }
    }
}