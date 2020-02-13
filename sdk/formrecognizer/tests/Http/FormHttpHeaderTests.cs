// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Http;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Http
{
    public class FormHttpHeaderTests
    {
        [Theory]
        [InlineData("application/pdf")]
        [InlineData("image/png")]
        [InlineData("image/jpeg")]
        [InlineData("image/tiff")]
        public void FormHttpHeader_ContentTypes(string expectContentType)
        {
            // Act
            var header = FormHttpHeader.Common.ForContentType(new FormContentType(expectContentType));

            // Assert
            Assert.Equal("Content-Type", header.Name, ignoreCase: true);
            Assert.Equal(expectContentType, header.Value);
        }
    }
}