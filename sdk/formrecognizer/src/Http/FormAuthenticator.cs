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
        private readonly CognitiveKeyCredential _keyCredential;
        private readonly CognitiveHeaderCredential _headerCredential;
        private readonly TokenCredential _tokenCredential;

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
            _tokenCredential = tokenCredential;
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
            else if (_tokenCredential != default)
            {
                await AuthenticateTokenCredentialAsync(request).ConfigureAwait(false);
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
            else if (_tokenCredential != default)
            {
                AuthenticateTokenCredential(request);
            }
            ProcessNext(message, pipeline);
        }

        private void AuthenticateTokenCredential(Request request)
        {
            var context = new TokenRequestContext(); // TODO make this static? Is it thread-safe?
            var accessToken = _tokenCredential.GetToken(context, default); // TODO cache the token
            UpdateRequest(accessToken, request);
        }

        private Task AuthenticateTokenCredentialAsync(Request request)
        {
            var context = new TokenRequestContext(); // TODO make this static? Is it thread-safe?
            var accessToken = _tokenCredential.GetToken(context, default); // TODO cache the token
            UpdateRequest(accessToken, request);
            return Task.CompletedTask;
        }

        private static void UpdateRequest(AccessToken accessToken, Request request)
        {
            request.Headers.SetValue(HttpHeader.Names.Authorization, $"Bearer {accessToken.Token}");
        }
    }
}