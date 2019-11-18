// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Rest;

    /// <summary>
    /// Allows authentication to the API using a basic apiKey mechanism
    /// </summary>
    public class FormClientCredentials : ServiceClientCredentials
    {
        private readonly string _subscriptionKey;

        /// <summary>
        /// Creates a new instance of the FormClientCredentials class
        /// </summary>
        /// <param name="subscriptionKey">The subscription key to authenticate and authorize as</param>
        public FormClientCredentials(string subscriptionKey)
        {
            _subscriptionKey = subscriptionKey;
        }

        /// <summary>
        /// Add the Basic Authentication Header to each outgoing request
        /// </summary>
        /// <param name="request">The outgoing request</param>
        /// <param name="cancellationToken">A token to cancel the operation</param>
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            request.Headers.Add("Ocp-Apim-Subscription-Key", _subscriptionKey);
            return Task.FromResult<object>(null);
        }
    }
}