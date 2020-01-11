// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Http
{
    internal class FormHttpPolicy : HttpPipelinePolicy
    {
        private const string ApimAuthenticationHeader = "Ocp-Apim-Subscription-Key";
        private const string FormRecognizerPathRoot = "formrecognizer";

        private readonly Uri _endpoint;
        private readonly string _basePath;
        private readonly string _userAgent;
        private readonly HttpHeader[] _extraHeaders;

        public string ApiKey { get; set; }

        public FormHttpPolicy(Uri endpoint, string apiKey, FormRecognizerClientOptions options)
        {
            ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
            _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
            var versionSegment = options.GetVersionString();
            _basePath = $"/{FormRecognizerPathRoot}/{versionSegment}";
            _userAgent = options.UserAgent;
            _extraHeaders = options.ExtraHeaders;
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
            message.Request.Headers.SetValue(ApimAuthenticationHeader, ApiKey);
            message.Request.Headers.SetValue(FormHttpHeader.Names.ClientRequestId, Guid.NewGuid().ToString());

            if (string.IsNullOrEmpty(message.Request.Uri.Host))
            {
                var sep = message.Request.Uri.Path.Length > 0 && message.Request.Uri.Path[0] == '/' ? String.Empty : "/";
                message.Request.Uri.Scheme = _endpoint.Scheme;
                message.Request.Uri.Host = _endpoint.Host;
                message.Request.Uri.Port = _endpoint.Port;
                message.Request.Uri.Path = _basePath + sep + message.Request.Uri.Path;
            }

            if (_userAgent != default)
            {
                message.Request.Headers.SetValue(HttpHeader.Names.UserAgent, _userAgent);
            }

            if (_extraHeaders != default)
            {
                foreach (var header in _extraHeaders)
                {
                    message.Request.Headers.Add(header);
                }
            }
        }
    }
}