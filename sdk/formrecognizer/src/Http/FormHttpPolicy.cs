// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Arguments;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Http
{
    internal class FormHttpPolicy : HttpPipelinePolicy
    {
        private const string FormRecognizerPathRoot = "formrecognizer";

        private readonly Uri _basePath;

        public FormHttpPolicy(Uri endpoint, FormClientOptions.ServiceVersion serviceVersion)
            : this(endpoint, FormClientOptions.GetVersionString(serviceVersion))
        {
        }

        private FormHttpPolicy(Uri endpoint, string versionSegment)
        {
            Throw.IfMissing(endpoint, nameof(endpoint));
            _basePath = new Uri(endpoint, $"/{FormRecognizerPathRoot}/{versionSegment}");
        }

        public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            UpdateMessage(message);
            ProcessNext(message, pipeline);
        }

        public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            UpdateMessage(message);
            await ProcessNextAsync(message, pipeline).ConfigureAwait(false);
        }

        private void UpdateMessage(HttpMessage message)
        {
            message.Request.Headers.SetValue(FormHttpHeader.Names.ClientRequestId, Guid.NewGuid().ToString());

            if (string.IsNullOrEmpty(message.Request.Uri.Host))
            {
                var sep = message.Request.Uri.Path.Length > 0 && message.Request.Uri.Path[0] == '/' ? String.Empty : "/";
                message.Request.Uri.Scheme = _basePath.Scheme;
                message.Request.Uri.Host = _basePath.Host;
                message.Request.Uri.Port = _basePath.Port;
                message.Request.Uri.Path = _basePath.AbsolutePath + sep + message.Request.Uri.Path;
            }
        }
    }
}