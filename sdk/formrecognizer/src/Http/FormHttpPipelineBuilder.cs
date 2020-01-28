// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Arguments;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Http
{
    internal static class FormHttpPipelineBuilder
    {
        public static HttpPipeline Build(Uri endpoint, FormAuthenticator authenticator, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(options, nameof(options));
            var endpointPolicy = new FormHttpPolicy(endpoint, options.Version);
            return HttpPipelineBuilder.Build(options, authenticator, endpointPolicy);
        }

        public static HttpPipeline Build(Uri endpoint, FormAuthenticator authenticator, FormLayoutClientOptions options)
        {
            Throw.IfMissing(options, nameof(options));
            var endpointPolicy = new FormHttpPolicy(endpoint, options.Version);
            return HttpPipelineBuilder.Build(options, authenticator, endpointPolicy);
        }

        public static HttpPipeline Build(Uri endpoint, FormAuthenticator authenticator, FormReceiptClientOptions options)
        {
            Throw.IfMissing(options, nameof(options));
            var endpointPolicy = new FormHttpPolicy(endpoint, options.Version);
            return HttpPipelineBuilder.Build(options, authenticator, endpointPolicy);
        }
    }
}