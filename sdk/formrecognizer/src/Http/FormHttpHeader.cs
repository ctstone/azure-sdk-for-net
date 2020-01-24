// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;

namespace Azure.AI.FormRecognizer.Http
{
    internal readonly struct FormHttpHeader
    {
        public static class Names
        {
            public static string ClientRequestId => "client-request-id";
        }

        public static class Common
        {
            public static HttpHeader ForContentType(FormContentType contentType)
            {
                return new HttpHeader(HttpHeader.Names.ContentType, contentType.ToString());
            }
        }
    }
}