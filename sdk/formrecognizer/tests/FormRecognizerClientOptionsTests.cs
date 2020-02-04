// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using Azure.AI.FormRecognizer.Http;
using Azure.Core;
using Xunit;
using static Azure.AI.FormRecognizer.FormClientOptions;

namespace Azure.AI.FormRecognizer.Tests
{
    public class FormRecognizerClientOptionsTests
    {
        [Fact]
        public void Constructor_Handles_DefaultParameters()
        {
            // Act
            var options = new FormClientOptions();

            // Assert
            Assert.Equal(Encoding.UTF8, options.Encoding);
            Assert.Equal(FormClientOptions.LatestVersion, options.Version);
            Assert.NotNull(FormClientOptions.GetVersionString(options.Version));
        }

        [Fact]
        public void Constructor_Handles_VersionParameter()
        {
            // Arrange
            var version = ServiceVersion.V2_0_Preview;
            var options = new FormClientOptions(version: version);

            // Assert
            Assert.Equal(version, options.Version);
        }
    }
}