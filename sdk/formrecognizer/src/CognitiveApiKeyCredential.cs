// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// A <see cref="CognitiveApiKeyCredential"/> is a subscription key used to authenticate the Form Recognizer service.
    /// It provides the ability to update the subscription key without creating a new client.
    /// </summary>
    public class CognitiveApiKeyCredential
    {
        private const string ApimKeyHeader = "Ocp-Apim-Subscription-Key";
        private volatile string _subscriptionKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="CognitiveApiKeyCredential"/> class.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key to use to authenticate with the service.</param>
        public CognitiveApiKeyCredential(string subscriptionKey) => UpdateCredential(subscriptionKey);

        /// <summary>
        /// Updates the Cognitive Service subscription key.
        /// This is intended to be used when you've regenerated your service subscription key
        /// and want to update long lived clients.
        /// </summary>
        /// <param name="subscriptionKey">Subscription key to athenticate the service against.</param>
        public void UpdateCredential(string subscriptionKey) => _subscriptionKey = subscriptionKey;

        internal void Authenticate(Request request) => UpdateRequest(request);

        internal Task AuthenticateAsync(Request request)
        {
            UpdateRequest(request);
            return Task.CompletedTask;
        }

        private void UpdateRequest(Request request)
        {
            request.Headers.Add(ApimKeyHeader, _subscriptionKey);
        }
    }
}