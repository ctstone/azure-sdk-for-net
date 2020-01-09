// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Pipeline
{
    internal class ApimAuthenticationPolicy : HttpPipelinePolicy
    {
        private const string ApimAuthenticationHeader = "Ocp-Apim-Subscription-Key";
        private const string FormRecognizerPathRoot = "formrecognizer";

        private readonly string _apiKey;
        private readonly Uri _endpoint;
        private readonly string _basePath;

        public ApimAuthenticationPolicy(Uri endpoint, string apiKey, FormRecognizerClientOptions.ServiceVersion version)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            var versionSegment = FormRecognizerClientOptions.GetVersionString(version);
            _basePath = $"/{FormRecognizerPathRoot}/{versionSegment}";
        }

        public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            UpdateMessage(message);
            ProcessNext(message, pipeline);
        }

        public override ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            UpdateMessage(message);
            return ProcessNextAsync(message, pipeline);
        }

        private void UpdateMessage(HttpMessage message)
        {
            var sep = message.Request.Uri.Path.Length > 0 && message.Request.Uri.Path[0] == '/' ? String.Empty : "/";
            message.Request.Headers.SetValue(ApimAuthenticationHeader, _apiKey);
            message.Request.Uri.Scheme = _endpoint.Scheme;
            message.Request.Uri.Host = _endpoint.Host;
            message.Request.Uri.Port = _endpoint.Port;
            message.Request.Uri.Path = _basePath + sep + message.Request.Uri.Path;
        }
    }
}