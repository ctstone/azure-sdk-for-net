// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Http;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Http
{
    public class FormHttpHeaderTests
    {
        [Theory]
        [InlineData(FormContentType.Pdf, "application/pdf")]
        [InlineData(FormContentType.Png, "image/png")]
        [InlineData(FormContentType.Jpeg, "image/jpeg")]
        [InlineData(FormContentType.Tiff, "image/tiff")]
        public void FormHttpHeader_ContentTypes(FormContentType testContentType, string expectContentType)
        {
            // Act
            var header = FormHttpHeader.Common.ForContentType(testContentType);

            // Assert
            Assert.Equal("Content-Type", header.Name, ignoreCase: true);
            Assert.Equal(expectContentType, header.Value);
        }
    }
}