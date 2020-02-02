// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Arguments;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Http
{
    internal class FormAuthenticator : HttpPipelinePolicy
    {
        private const string DefaultCognitiveScope = "https://cognitiveservices.azure.com/.default";
        private readonly CognitiveKeyCredential _keyCredential;
        private readonly CognitiveHeaderCredential _headerCredential;
        private readonly BearerTokenAuthenticationPolicy _bearerPolicy;

        public FormAuthenticator(CognitiveKeyCredential keyCredential)
        {
            Throw.IfMissing(keyCredential, nameof(keyCredential));
            _keyCredential = keyCredential;
        }

        public FormAuthenticator(CognitiveHeaderCredential headerCredential)
        {
            Throw.IfMissing(headerCredential, nameof(headerCredential));
            _headerCredential = headerCredential;
        }

        public FormAuthenticator(TokenCredential tokenCredential)
        {
            Throw.IfMissing(tokenCredential, nameof(tokenCredential));
            _bearerPolicy = new BearerTokenAuthenticationPolicy(tokenCredential, DefaultCognitiveScope);
        }

        public override async ValueTask ProcessAsync(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            var request = message.Request;
            if (_keyCredential != default)
            {
                await _keyCredential.AuthenticateAsync(request).ConfigureAwait(false);
            }
            else if (_headerCredential != default)
            {
                await _headerCredential.AuthenticateAsync(request).ConfigureAwait(false);
            }
            else if (_bearerPolicy != default)
            {
                await _bearerPolicy.ProcessAsync(message, pipeline).ConfigureAwait(false);
                return;
            }

            await ProcessNextAsync(message, pipeline).ConfigureAwait(false);
        }

        public override void Process(HttpMessage message, ReadOnlyMemory<HttpPipelinePolicy> pipeline)
        {
            var request = message.Request;
            if (_keyCredential != default)
            {
                _keyCredential.Authenticate(request);
            }
            else if (_headerCredential != default)
            {
                _headerCredential.Authenticate(request);
            }
            else if (_bearerPolicy != default)
            {
                _bearerPolicy.Process(message, pipeline);
                return;
            }
            ProcessNext(message, pipeline);
        }
    }
}