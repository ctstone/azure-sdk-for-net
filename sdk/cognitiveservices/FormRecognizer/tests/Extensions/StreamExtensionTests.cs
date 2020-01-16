// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;
using Azure.AI.FormRecognizer.Extensions;

namespace Azure.AI.FormRecognizer.Tests.Extensions
{
    public class StreamExtensionTests
    {
        [Theory]
        [InlineData(true, new byte[] { (byte)'%', (byte)'P', (byte)'D', (byte)'F', (byte)'-', (byte)'9', 1, 2, 3 }, FormContentType.Pdf)]
        [InlineData(true, new byte[] { 0x89, (byte)'P', (byte)'N', (byte)'G', 1, 2, 3 }, FormContentType.Png)]
        [InlineData(true, new byte[] { 0xff, 0xd8 }, FormContentType.Jpeg)]
        [InlineData(true, new byte[] { (byte)'M', (byte)'M' }, FormContentType.Tiff)]
        [InlineData(true, new byte[] { (byte)'I', (byte)'I' }, FormContentType.Tiff)]
        [InlineData(false, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, null)]
        [InlineData(false, new byte[] { (byte)'%', (byte)'P', (byte)'D', (byte)'F' }, null)]
        [InlineData(false, new byte[] { 0x88, (byte)'P', (byte)'N', (byte)'G', 1, 2, 3 }, null)]
        [InlineData(false, new byte[0], null)]
        public void TryGetContentType_Returns_ContentType(bool expectValid, byte[] data, FormContentType? expectContentType)
        {
            // Arrange
            var stream = new MemoryStream(data);
            FormContentType? contentType;

            // Act
            var isValid = stream.TryGetContentType(out contentType);

            // Assert
            Assert.Equal(expectValid, isValid);
            Assert.Equal(expectContentType, contentType);
        }
    }
}