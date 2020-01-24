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
            Assert.NotNull(options.ExtraHeaders);
            Assert.NotNull(options.GetVersionString());
        }

        [Fact]
        public void Constructor_Handles_VersionParameter()
        {
            // Arrange
            var version = ServiceVersion.V2_0_Preview;
            var options = new FormRecognizerClientOptions(version: version);

            // Assert
            Assert.Equal(version, options.Version);
        }
    }
}