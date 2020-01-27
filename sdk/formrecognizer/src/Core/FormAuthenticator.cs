// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Core
{
    internal class FormAuthenticator
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

        public void Authenticate(Request request)
        {
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
        }

        public async Task AuthenticateAsync(Request request)
        {
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