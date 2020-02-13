// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using Xunit;
using Azure.AI.FormRecognizer.Extensions;
using System;

namespace Azure.AI.FormRecognizer.Tests.Extensions
{
    public class StreamExtensionTests
    {
        [Theory]
        [InlineData(true, new byte[] { (byte)'%', (byte)'P', (byte)'D', (byte)'F', (byte)'-', (byte)'9', 1, 2, 3 }, "application/pdf")]
        [InlineData(true, new byte[] { 0x89, (byte)'P', (byte)'N', (byte)'G', 1, 2, 3 }, "image/png")]
        [InlineData(true, new byte[] { 0xff, 0xd8 }, "image/jpeg")]
        [InlineData(true, new byte[] { (byte)'M', (byte)'M' }, "image/tiff")]
        [InlineData(true, new byte[] { (byte)'I', (byte)'I' }, "image/tiff")]
        [InlineData(false, new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, null)]
        [InlineData(false, new byte[] { (byte)'%', (byte)'P', (byte)'D', (byte)'F' }, null)]
        [InlineData(false, new byte[] { 0x88, (byte)'P', (byte)'N', (byte)'G', 1, 2, 3 }, null)]
        [InlineData(false, new byte[0], null)]
        public void TryGetContentType_Returns_ContentType(bool expectValid, byte[] data, string expectContentType)
        {
            Console.WriteLine(" >>>> " + expectContentType);

            // Arrange
            var stream = new MemoryStream(data);
            FormContentType? contentType;

            // Act
            var isValid = stream.TryGetContentType(out contentType);

            // Assert
            Assert.Equal(expectValid, isValid);
            Assert.Equal(expectContentType, contentType.HasValue ? contentType.Value : null);
        }
    }
}