// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Http;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// The FormRecognizer client provides syncronous and asynchronous methods to manage custom forms,
    /// prebuilt models, and layout requests.
    /// </summary>
    public class FormRecognizerClient
    {
        private readonly CustomFormClient _customFormClient;
        private readonly PrebuiltFormClient _prebuiltFormClient;
        private readonly FormLayoutClient _layoutClient;

        /// <summary>
        /// Access custom form models.
        /// </summary>
        public virtual CustomFormClient Custom => _customFormClient;

        /// <summary>
        /// Access the prebuilt models.
        /// </summary>
        public virtual PrebuiltFormClient Prebuilt => _prebuiltFormClient;

        /// <summary>
        /// Access form layout models.
        /// </summary>
        public virtual FormLayoutClient Layout => _layoutClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        public FormRecognizerClient(Uri endpoint, CognitiveKeyCredential credential)
            : this(endpoint, credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        /// <param name="options">Optional service parameters.</param>
        public FormRecognizerClient(Uri endpoint, CognitiveKeyCredential credential, FormRecognizerClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        public FormRecognizerClient(Uri endpoint, TokenCredential credential)
            : this(endpoint, credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        /// <param name="options">Optional service parameters.</param>
        public FormRecognizerClient(Uri endpoint, TokenCredential credential, FormRecognizerClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">User-defined credential.</param>
        public FormRecognizerClient(Uri endpoint, CognitiveHeaderCredential credential)
            : this(endpoint, credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">User-defined credential</param>
        /// <param name="options">Optional service parameters.</param>
        public FormRecognizerClient(Uri endpoint, CognitiveHeaderCredential credential, FormRecognizerClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        internal FormRecognizerClient(Uri endpoint, FormAuthenticator authenticator, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(options, nameof(options));
            var authentication = new FormHttpPolicy(endpoint, authenticator, options.Version);
            var pipeline = HttpPipelineBuilder.Build(options, authentication);

            _customFormClient = new CustomFormClient(pipeline, options);
            _prebuiltFormClient = new PrebuiltFormClient(pipeline, options);
            _layoutClient = new FormLayoutClient(pipeline, options);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class for mocking.
        /// </summary>
        protected FormRecognizerClient()
        { }
    }
}
