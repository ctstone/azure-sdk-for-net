// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Arguments;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Http
{
    internal readonly struct FormHttpHeader
    {
        public static class Names
        {
            public static string ClientRequestId => "client-request-id";
            public static string SubscriptionKey => "Ocp-Apim-Subscription-Key";
        }

        public static class Common
        {
            public static HttpHeader ForContentType(FormContentType contentType)
            {
                return new HttpHeader(HttpHeader.Names.ContentType, contentType.ToString());
            }

            public static HttpHeader Authorize(string value)
            {
                Throw.IfNullOrEmpty(value, nameof(value));
                return new HttpHeader(FormHttpHeader.Names.SubscriptionKey, value);
            }
        }
    }
}