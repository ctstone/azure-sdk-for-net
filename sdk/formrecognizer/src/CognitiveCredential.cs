// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Http;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Set credentials for the Form Recognizer service
    /// </summary>
    public class CognitiveCredential
    {
        private readonly object _lock = new object();
        private CognitiveEndpoint _endpoint;
        private HttpHeader[] _headers;

        /// <summary>
        /// Get the current endpoint for this credential object.
        /// </summary>
        public Uri Endpoint => _endpoint;

        internal HttpHeader[] Headers => _headers;

        /// <summary>
        /// Initializes a new instance of the <see cref="CognitiveCredential"/> class.
        /// </summary>
        /// <param name="endpoint">
        /// Use the correct endpoint that matches your `subscriptionKey`:
        ///
        /// ```
        /// var endpoint = CognitiveEndpoint.EastUnitedStates
        /// ```
        /// </param>
        /// <param name="subscriptionKey">A Cognitive Services subscription key, obtained from https://portal.azure.com.</param>
        public CognitiveCredential(CognitiveEndpoint endpoint, string subscriptionKey)
            : this(endpoint, FormHttpHeader.Common.Authorize(subscriptionKey))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CognitiveCredential"/> class for private containers.
        /// </summary>
        /// <param name="endpoint">
        /// Point to your own private container host:
        ///
        /// ```csharp
        /// var containerUri = new Uri("http://mycontainer");
        /// var endpoint = new CognitiveEndpoint(containerUri)
        /// </param>
        /// <param name="headers">Any optional headers to pass to your container host.</param>
        public CognitiveCredential(CognitiveEndpoint endpoint, params HttpHeader[] headers)
        {
            _endpoint = endpoint;
            _headers = headers ?? throw new ArgumentNullException(nameof(headers));
        }

        /// <summary>
        /// Thread-safe update of the stored Cognitive Service credentials.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="subscriptionKey"></param>
        public virtual void Refresh(CognitiveEndpoint endpoint, string subscriptionKey)
        {
            Refresh(endpoint, FormHttpHeader.Common.Authorize(subscriptionKey));
        }

        /// <summary>
        /// Thread-safe update of the stored Cognitive Service credentials.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="headers"></param>
        public virtual void Refresh(CognitiveEndpoint endpoint, params HttpHeader[] headers)
        {
            Throw.IfMissing(headers, nameof(headers));
            lock (_lock)
            {
                _endpoint = endpoint;
                _headers = headers;
            }
        }

        internal void Authorize(Request request) => AuthorizeRequest(request);

        internal virtual Task AuthorizeAsync(Request request, CancellationToken cancellationToken) => AuthorizeRequestAsync(request, cancellationToken);

        /// <summary>
        /// Authorize a request.
        /// </summary>
        /// <param name="request">The request to authorize</param>
        protected virtual void AuthorizeRequest(Request request) => UpdateRequest(request);

        /// <summary>
        /// Asynchronously authorize a request.
        /// </summary>
        /// <param name="request">The request to authorize</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        protected virtual Task AuthorizeRequestAsync(Request request, CancellationToken cancellationToken)
        {
            UpdateRequest(request);
            return Task.CompletedTask;
        }

        private void UpdateRequest(Request request)
        {
            Throw.IfMissing(request, nameof(request));
            foreach (var header in _headers)
            {
                request.Headers.Add(header);
            }
        }
    }
}