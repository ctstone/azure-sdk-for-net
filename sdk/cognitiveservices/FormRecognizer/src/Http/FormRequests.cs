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
    internal static class FormRequests
    {
        public static Request CreateAnalyzeStreamRequest(HttpPipeline pipeline, string basePath, Stream stream, FormContentType? contentType, bool? includeTextDetails)
        {
            Throw.IfMissing(stream, nameof(stream));
            return CreateAnalyzeRequest(pipeline, basePath, includeTextDetails, contentType: contentType, stream: stream);
        }

        public static Request CreateAnalyzeUriRequest(HttpPipeline pipeline, string basePath, Uri uri, bool? includeTextDetails, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(uri, nameof(uri));
            return CreateAnalyzeRequest(pipeline, basePath, includeTextDetails, options: options, uri: uri);
        }

        public static Request CreateGetAnalysisRequest(HttpPipeline pipeline, string basePath, string id)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri.Path = $"{basePath}/analyzeResults/{id}";
            return request;
        }

        private static Request CreateAnalyzeRequest(HttpPipeline pipeline, string basePath, bool? includeTextDetails, FormRecognizerClientOptions options = default, Uri uri = default, Stream stream = default, FormContentType? contentType = default)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Post;
            request.Uri.Path = $"{basePath}/analyze";

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