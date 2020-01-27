// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.Core;
using Azure.Core.Pipeline;
using static Azure.AI.FormRecognizer.FormRecognizerClientOptions;

namespace Azure.AI.FormRecognizer.Http
{
    internal class FormHttpPolicy : HttpPipelinePolicy
    {
        private const string FormRecognizerPathRoot = "formrecognizer";

        private readonly Uri _basePath;
        private readonly FormAuthenticator _authenticator;

        public FormHttpPolicy(Uri endpoint, FormAuthenticator authenticator, ServiceVersion serviceVersion)
        {
            Throw.IfMissing(endpoint, nameof(endpoint));
            Throw.IfMissing(authenticator, nameof(authenticator));
            _authenticator = authenticator;
            var versionSegment = GetVersionString(serviceVersion);
            _basePath = new Uri(endpoint, $"/{FormRecognizerPathRoot}/{versionSegment}");
        }

        public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            _authenticator.Authenticate(message.Request);
            UpdateMessage(message);
            ProcessNext(message, pipeline);
        }

        public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            await _authenticator.AuthenticateAsync(message.Request).ConfigureAwait(false);
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