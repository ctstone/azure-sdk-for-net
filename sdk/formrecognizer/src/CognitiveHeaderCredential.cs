// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// A <see cref="CognitiveHeaderCredential"/> is a user-defined authentication method to supply custom credentials
    /// to the Form Recognizer service. It provides the ability to update the headers without creating a new client.
    /// </summary>
    public class CognitiveHeaderCredential
    {
        private readonly object _lock = new object();
        private HttpHeader[] _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CognitiveHeaderCredential"/> class.
        /// </summary>
        /// <param name="headers">User-defined headers to send to the Cognitive Service.</param>
        public CognitiveHeaderCredential(params HttpHeader[] headers) => UpdateCredential(headers);

        /// <summary>
        /// Updates the Cognitive Service subscription key.
        /// This is intended to be used when you've regenerated your service subscription key
        /// and want to update long lived clients.
        /// </summary>
        /// <param name="headers">Subscription key to athenticate the service against.</param>
        public void UpdateCredential(HttpHeader[] headers)
        {
            lock (_lock)
            {
                _headers = headers;
            }
        }

        internal void Authenticate(Request request)
        {
            UpdateRequest(request);
        }

        internal Task AuthenticateAsync(Request request)
        {
            UpdateRequest(request);
            return Task.CompletedTask;
        }

        private void UpdateRequest(Request request)
        {
            lock (_lock)
            {
                foreach (var header in _headers)
                {
                    request.Headers.Add(header);
                }
            }
        }
    }
}