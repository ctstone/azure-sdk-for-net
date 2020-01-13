// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Azure.AI.FormRecognizer.Core;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Extensions.Layout
{
    internal static class LayoutFormExtensions
    {
        private const string BasePath = "/layout";

        public static Request CreateAnalyzeStreamRequest(this HttpPipeline pipeline, Stream stream, FormContentType? contentType)
        {
            return FormRequests.CreateAnalyzeStreamRequest(pipeline, BasePath, stream, contentType, includeTextDetails: null);
        }

        public static Request CreateAnalyzeUriRequest(this HttpPipeline pipeline, Uri uri, FormRecognizerClientOptions options)
        {
            return FormRequests.CreateAnalyzeUriRequest(pipeline, BasePath, uri, includeTextDetails: null, options);
        }

        public static Request CreateGetAnalysisRequest(this HttpPipeline pipeline, string resultId)
        {
            return FormRequests.CreateGetAnalysisRequest(pipeline, BasePath, resultId);
        }
    }
}