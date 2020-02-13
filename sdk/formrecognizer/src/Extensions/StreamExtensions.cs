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
            contentType = new Nullable<FormContentType>();
            var maxBytes = Math.Min(stream.Length, PdfHeader.Length);
            var isPdf = true;
            var isPng = true;
            var isJpeg = true;
            var isTiff = true;
            var originalPosition = stream.Position;

            byte b;
            Func<byte[], int, bool> isAtEnd = (array, i) => i == array.Length - 1;
            Func<byte[], int, bool> isBeyond = (array, i) => i >= array.Length;
            for (var i = 0; i < maxBytes; i += 1, stream.Position = i)
            {
                b = (byte)stream.ReadByte();

                var endOfPdf = isAtEnd(PdfHeader, i);
                var beyondPdf = isBeyond(PdfHeader, i);
                isPdf &= !beyondPdf && PdfHeader[i] == b;
                if (isPdf && endOfPdf)
                {
                    contentType = FormContentType.Pdf;
                    break;
                }

                var endOfPng = isAtEnd(PngHeader, i);
                var beyondPng = isBeyond(PngHeader, i);
                isPng &= !beyondPng && PngHeader[i] == b;
                if (isPng && endOfPng)
                {
                    contentType = FormContentType.Png;
                    break;
                }

                var endOfJpeg = isAtEnd(JpegHeader, i);
                var beyondJpeg = isBeyond(JpegHeader, i);
                isJpeg &= !beyondJpeg && JpegHeader[i] == b;
                if (isJpeg && endOfJpeg)
                {
                    contentType = FormContentType.Jpeg;
                    break;
                }

                var endOfTiffLE = isAtEnd(TiffHeaderLE, i);
                var endOfTiffBE = isAtEnd(TiffHeaderBE, i);
                var beyondTiffLE = isBeyond(TiffHeaderLE, i);
                var beyondTiffBE = isBeyond(TiffHeaderBE, i);
                isTiff &= ((!beyondTiffLE && TiffHeaderLE[i] == b) || (!beyondTiffBE && TiffHeaderBE[i] == b));
                if (isTiff && (endOfTiffLE || endOfTiffBE))
                {
                    contentType = FormContentType.Tiff;
                    break;
                }
            }

            stream.Position = originalPosition;
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