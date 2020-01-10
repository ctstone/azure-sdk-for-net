// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    internal readonly struct FormHttpHeader
    {
        public static class Common
        {
            public static readonly HttpHeader PdfContentType
                = new HttpHeader(HttpHeader.Names.ContentType, "application/pdf");
            public static readonly HttpHeader PngContentType
                = new HttpHeader(HttpHeader.Names.ContentType, "image/png");
            public static readonly HttpHeader JpegContentType
                = new HttpHeader(HttpHeader.Names.ContentType, "image/jpeg");
            public static readonly HttpHeader TiffContentType
                = new HttpHeader(HttpHeader.Names.ContentType, "image/tiff");

            public static HttpHeader ForContentType(FormContentType contentType)
            {
                return contentType switch
                {
                    FormContentType.PDF => PdfContentType,
                    FormContentType.PNG => PngContentType,
                    FormContentType.JPEG => JpegContentType,
                    FormContentType.TIFF => TiffContentType,
                    _ => throw new NotSupportedException($"Content-Type {contentType} is not supported."),
                };
            }
        }
    }
}