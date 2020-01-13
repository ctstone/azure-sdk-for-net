// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Azure.AI.FormRecognizer.Core;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class PipelineExtensions
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
            request.Uri.Path = CustomFormModelClient.GetModelPath(modelId);
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
            request.Uri.Path = CustomFormModelClient.GetModelPath(modelId);
            return request;
        }

        public static Request CreateAnalyzeStreamRequest<TOptions>(this HttpPipeline pipeline, string basePath, Stream stream, FormContentType? contentType, TOptions? analyzeOptions, Action<TOptions, Request> applyOptions)
            where TOptions : struct
        {
            Throw.IfMissing(stream, nameof(stream));
            return CreateAnalyzeRequest(pipeline, basePath, analyzeOptions, applyOptions, contentType: contentType, stream: stream);
        }

        public static Request CreateAnalyzeUriRequest<TOptions>(this HttpPipeline pipeline, string basePath, Uri uri, FormRecognizerClientOptions options, TOptions? analyzeOptions, Action<TOptions, Request> applyOptions)
            where TOptions : struct
        {
            Throw.IfMissing(uri, nameof(uri));
            return CreateAnalyzeRequest(pipeline, basePath, analyzeOptions, applyOptions, options: options, uri: uri);
        }

        public static Request CreateGetAnalysisRequest(this HttpPipeline pipeline, string basePath, string id)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri.Path = $"{basePath}/analyzeResults/{id}";
            return request;
        }

        private static Request CreateAnalyzeRequest<TOptions>(HttpPipeline pipeline, string basePath, TOptions? analyzeOptions, Action<TOptions, Request> applyOptions, FormRecognizerClientOptions options = default, Uri uri = default, Stream stream = default, FormContentType? contentType = default)
            where TOptions : struct
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Post;
            request.Uri.Path = $"{basePath}/analyze";

            if (analyzeOptions != null)
            {
                applyOptions(analyzeOptions.Value, request);
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