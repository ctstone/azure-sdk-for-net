// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Azure.AI.FormRecognizer.Core;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Extensions.Prebuilt
{
    internal static class PrebuiltFormExtensions
    {
        public static Request CreateAnalyzeStreamRequest(this HttpPipeline pipeline, string modelName, Stream stream, FormContentType? contentType, bool? includeTextDetails)
        {
            return FormRequests.CreateAnalyzeStreamRequest(pipeline, PrebuiltFormClient.GetModelPath(modelName), stream, contentType, includeTextDetails);
        }

        public static Request CreateAnalyzeUriRequest(this HttpPipeline pipeline, string modelName, Uri uri, bool? includeTextDetails, FormRecognizerClientOptions options)
        {
            return FormRequests.CreateAnalyzeUriRequest(pipeline, PrebuiltFormClient.GetModelPath(modelName), uri, includeTextDetails, options);
        }

        public static Request CreateGetAnalysisRequest(this HttpPipeline pipeline, string modelName, string resultId)
        {
            return FormRequests.CreateGetAnalysisRequest(pipeline, PrebuiltFormClient.GetModelPath(modelName), resultId);
        }
    }
}