// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class StreamExtensions
    {
        private const string DefaultSeekErrorMessage = "Stream is not seekable.";
        private const string DefaultReadErrorMessage = "Stream is not readable.";
        private const string DefaultContentTypeErrorMessage = "Cannot determine Content-Type of stream.";
        public static bool IsPdfStream(this Stream stream)
        {
            throw new NotImplementedException();
        }

        public static bool IsPngStream(this Stream stream)
        {
            throw new NotImplementedException();
        }

        public static bool IsJpegStream(this Stream stream)
        {
            throw new NotImplementedException();
        }

        public static bool IsTiffStream(this Stream stream)
        {
            throw new NotImplementedException();
        }

        public static bool TryGetContentType(this Stream stream, out FormContentType? contentType)
        {
            if (stream.IsPdfStream())
            {
                contentType = FormContentType.PDF;
            }
            else if (stream.IsJpegStream())
            {
                contentType = FormContentType.JPEG;
            }
            else if (stream.IsPngStream())
            {
                contentType = FormContentType.PNG;
            }
            else if (stream.IsTiffStream())
            {
                contentType = FormContentType.TIFF;
            }
            else
            {
                contentType = null;
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