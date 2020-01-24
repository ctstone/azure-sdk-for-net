// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using static Azure.AI.FormRecognizer.FormRecognizerClientOptions;

namespace Azure.AI.FormRecognizer.Http
{
    internal class FormHttpPolicy : HttpPipelinePolicy
    {
        private const string FormRecognizerPathRoot = "formrecognizer";

        private readonly string _basePath; // TODO: Uri?
        private readonly CognitiveCredential _credential;

        public CognitiveCredential Credential => _credential;

        public FormHttpPolicy(CognitiveCredential credential, ServiceVersion serviceVersion)
        {
            _credential = credential ?? throw new ArgumentNullException(nameof(credential));
            var versionSegment = GetVersionString(serviceVersion);
            _basePath = $"/{FormRecognizerPathRoot}/{versionSegment}";
        }

        public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            _credential.Authorize(message.Request);
            UpdateMessage(message);
            ProcessNext(message, pipeline);
        }

        public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            await _credential.AuthorizeAsync(message.Request, default).ConfigureAwait(false);
            UpdateMessage(message);
            await ProcessNextAsync(message, pipeline).ConfigureAwait(false);
        }

        private void UpdateMessage(HttpMessage message)
        {
            message.Request.Headers.SetValue(FormHttpHeader.Names.ClientRequestId, Guid.NewGuid().ToString());

            if (string.IsNullOrEmpty(message.Request.Uri.Host))
            {
                var sep = message.Request.Uri.Path.Length > 0 && message.Request.Uri.Path[0] == '/' ? String.Empty : "/";
                message.Request.Uri.Scheme = _credential.Endpoint.Scheme;
                message.Request.Uri.Host = _credential.Endpoint.Host;
                message.Request.Uri.Port = _credential.Endpoint.Port;
                message.Request.Uri.Path = _basePath + sep + message.Request.Uri.Path;
            }
        }

        internal static string GetVersionString(ServiceVersion version)
        {
            return version switch
            {
                ServiceVersion.V2_0_Preview => "v2.0-preview",
                _ => throw new NotSupportedException($"The service version {version} is not supported."),
            };
        }
    }
}