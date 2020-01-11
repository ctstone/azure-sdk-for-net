// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.IO;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class CustomFormExtensions
    {
        public static Request CreateTrainRequest(this HttpPipeline pipeline, TrainingRequest trainRequest, FormRecognizerClientOptions options)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Post;
            request.Uri.Path = "/custom/models";
            request.AddJsonContent(trainRequest, options);
            return request;
        }

        public static Request CreateGetModelRequest(this HttpPipeline pipeline, string modelId)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri.Path = $"/custom/models/{modelId}";
            return request;
        }

        public static Request CreateListModelsRequest(this HttpPipeline pipeline, string nextLink = default, string op = default)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            if (!string.IsNullOrEmpty(op))
            {
                request.Uri.AppendQuery("op", op);
            }
            if (string.IsNullOrEmpty(nextLink))
            {
                request.Uri.Path = "/custom/models";
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
            request.Uri.Path = $"/custom/models/{modelId}";
            return request;
        }

        public static Request CreateAnalyzeStreamRequest(this HttpPipeline pipeline, string modelId, Stream stream, FormContentType? contentType, bool? includeTextDetails)
        {
            Throw.IfMissing(stream, nameof(stream));
            return pipeline.CreateAnalyzeRequest(modelId, includeTextDetails, contentType: contentType, stream: stream);
        }

        public static Request CreateAnalyzeUriRequest(this HttpPipeline pipeline, string modelId, Uri uri, bool? includeTextDetails, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(uri, nameof(uri));
            return pipeline.CreateAnalyzeRequest(modelId, includeTextDetails, options: options, uri: uri);
        }

        public static Request CreateGetAnalysisRequest(this HttpPipeline pipeline, string modelId, string resultId)
        {
            Throw.IfMissing(modelId, nameof(modelId));
            Throw.IfMissing(resultId, nameof(resultId));
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri.Path = $"/custom/models/{modelId}/analyzeResults/{resultId}";
            return request;
        }

        private static Request CreateAnalyzeRequest(this HttpPipeline pipeline, string modelId, bool? includeTextDetails, FormRecognizerClientOptions options = default, Uri uri = default, Stream stream = default, FormContentType? contentType = default)
        {
            Throw.IfMissing(modelId, nameof(modelId));
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Post;
            request.Uri.Path = $"/custom/models/{modelId}/analyze";

            if (includeTextDetails != default)
            {
                request.Uri.AppendQuery("includeTextDetails", includeTextDetails.Value.ToString(CultureInfo.InvariantCulture));
            }

            if (uri != default)
            {
                var analysisRequest = new AnalysisRequest { Source = uri.OriginalString };
                request.AddJsonContent(analysisRequest, options);
            }
            else if (stream != default)
            {
                request.AddBinaryContent(contentType, stream);
            }
            else
            {
                throw new InvalidOperationException("Analysis request is missing required fields.");
            }

            return request;
        }
    }
}