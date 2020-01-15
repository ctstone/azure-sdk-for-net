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
            var options = new FormRecognizerClientOptions();
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

        [Fact]
        public void Constructor_Exposes_SerializationOptions()
        {
            // Arrange
            var options = new FormRecognizerClientOptions();

            // Assert
            Assert.NotNull(options.SerializationOptions);
        }
    }
}