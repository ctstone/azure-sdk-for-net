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
        private const string ApiKeyHeader = "Ocp-Apim-Subscription-Key";

        /// <summary>
        /// Creates a new instance of the ApiKeyServiceClientCredentails class
        /// </summary>
        /// <param name="subscriptionKey">The subscription key to authenticate and authorize as</param>
        public FormClientCredentials(string subscriptionKey)
        {
            this._subscriptionKey = subscriptionKey;
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

            request.Headers.Add(ApiKeyHeader, this._subscriptionKey);
            return Task.FromResult<object>(null);
        }
    }
}
