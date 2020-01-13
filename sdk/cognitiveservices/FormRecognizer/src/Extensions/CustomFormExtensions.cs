// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Azure.AI.FormRecognizer.Core;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Extensions.Custom
{
    internal static class CustomFormExtensions
    {
        private const string OperatorQueryKey = "op";

        public static Request CreateTrainRequest(this HttpPipeline pipeline, TrainingRequest trainRequest, FormRecognizerClientOptions options)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Post;
            request.Uri.Path = CustomFormClient.BasePath;
            request.AddJsonContent(trainRequest, options);
            return request;
        }

        public static Request CreateGetModelRequest(this HttpPipeline pipeline, string modelId)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri.Path = CustomFormClient.GetModelPath(modelId);
            return request;
        }

        public static Request CreateListModelsRequest(this HttpPipeline pipeline, string nextLink = default, string op = default)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            if (!string.IsNullOrEmpty(op))
            {
                request.Uri.AppendQuery(OperatorQueryKey, op);
            }
            if (string.IsNullOrEmpty(nextLink))
            {
                request.Uri.Path = CustomFormClient.BasePath;
            }
            else
            {
                request.Uri.Reset(new Uri(nextLink, UriKind.Absolute));
            }
            return request;
        }

        public static Request CreateDeleteModelRequest(this HttpPipeline pipeline, string modelId)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Delete;
            request.Uri.Path = CustomFormClient.GetModelPath(modelId);
            return request;
        }

        public static Request CreateAnalyzeStreamRequest(this HttpPipeline pipeline, string modelId, Stream stream, FormContentType? contentType, bool? includeTextDetails)
        {
            Throw.IfMissing(stream, nameof(stream));
            return FormRequests.CreateAnalyzeStreamRequest(pipeline, CustomFormClient.GetModelPath(modelId), stream, contentType, includeTextDetails);
        }

        public static Request CreateAnalyzeUriRequest(this HttpPipeline pipeline, string modelId, Uri uri, bool? includeTextDetails, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(uri, nameof(uri));
            return FormRequests.CreateAnalyzeUriRequest(pipeline, CustomFormClient.GetModelPath(modelId), uri, includeTextDetails, options);
        }

        public static Request CreateGetAnalysisRequest(this HttpPipeline pipeline, string modelId, string resultId)
        {
            return FormRequests.CreateGetAnalysisRequest(pipeline, CustomFormClient.GetModelPath(modelId), resultId);
        }


    }
}