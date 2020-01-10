// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Text;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class StreamExtensions
    {
        private const string DefaultSeekErrorMessage = "Stream is not seekable.";
        private const string DefaultReadErrorMessage = "Stream is not readable.";

        private static byte[] PdfHeader = Encoding.ASCII.GetBytes("%PDF-");
        private static byte[] PngHeader = new byte[] { 0x89, (byte)'P', (byte)'N', (byte)'G' };
        private static byte[] JpegHeader = new byte[] { 0xff, 0xd8 };
        private static byte[] TiffHeaderBE = Encoding.ASCII.GetBytes("MM");
        private static byte[] TiffHeaderLE = Encoding.ASCII.GetBytes("II");

        public static bool TryGetContentType(this Stream stream, out FormContentType? contentType)
        {
            var maxBytes = PdfHeader.Length;
            var minBytes = JpegHeader.Length;
            var isPdf = true;
            var isPng = true;
            var isJpeg = true;
            var isTiff = true;
            var originalPosition = stream.Position;

            if (stream.Length >= minBytes)
            {
                byte b;
                Func<byte[], int, bool> isAtEnd = (array, i) => i == array.Length - 1;
                for (var i = 0; i < maxBytes; i += 1, stream.Position = i)
                {
                    b = (byte)stream.ReadByte();
                    isPdf &= PdfHeader[i] == b;
                    if (isPdf && isAtEnd(PdfHeader, i))
                    {
                        break;
                    }
                    isPng &= PngHeader[i] == b;
                    if (isPng && isAtEnd(PngHeader, i))
                    {
                        break;
                    }
                    isJpeg &= JpegHeader[i] == b;
                    if (isJpeg && isAtEnd(JpegHeader, i))
                    {
                        break;
                    }
                    isTiff &= (TiffHeaderLE[i] == b || TiffHeaderBE[i] == b);
                    if (isTiff && (isAtEnd(TiffHeaderLE, i) || isAtEnd(TiffHeaderBE, i)))
                    {
                        break;
                    }
                }
            }
            else
            {
                isPdf = isPng = isJpeg = isTiff = false;
            }

            stream.Position = originalPosition;

            if (isPdf)
            {
                contentType = FormContentType.PDF;
            }
            else if (isPng)
            {
                contentType = FormContentType.PNG;
            }
            else if (isJpeg)
            {
                contentType = FormContentType.JPEG;
            }
            else if (isTiff)
            {
                contentType = FormContentType.TIFF;
            }
            else
            {
                contentType = new Nullable<FormContentType>();
            }

            return contentType.HasValue;
        }

        public static void ThrowIfCannotSeek(this Stream stream, string message = default, string paramName = default)
        {
            if (!stream.CanSeek)
            {
                throw new ArgumentException(message ?? DefaultSeekErrorMessage, paramName ?? nameof(stream));
            }
        }

        public static void ThrowIfCannotRead(this Stream stream, string message = default, string paramName = default)
        {
            if (!stream.CanRead)
            {
                throw new ArgumentException(message ?? DefaultReadErrorMessage, paramName ?? nameof(stream));
            }
        }
    }
}