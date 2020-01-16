// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Http;
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
        private readonly FormHttpPolicy _authentication;


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
        /// Get or set the api key. This value can be updated at any time.
        /// </summary>
        public virtual string ApiKey
        {
            get => _authentication.ApiKey;
            set
            {
                Throw.IfMissing(value, nameof(ApiKey));
                Throw.IfNullOrEmpty(value, nameof(ApiKey));
                _authentication.ApiKey = value;
            }
        }

        /// <summary>
        /// Get or set the base URI. For example, `https://eastus.cognitiveservices.com/`.
        ///
        /// This value can can be updated at any time.
        /// </summary>
        public virtual Uri Endpoint
        {
            get => _authentication.Endpoint;
            set
            {
                Throw.IfInvalidUri(value, nameof(Endpoint));
                _authentication.Endpoint = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">
        /// The base URI of the Form Recognizer Service. For example, `https://eastus.cognitiveservices.com/`.
        /// </param>
        /// <param name="apiKey">The service key, copied from the Azure Portal.</param>
        public FormRecognizerClient(Uri endpoint, string apiKey)
        : this(endpoint, apiKey, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint">
        /// The base url of the Form Recognizer Service. For example,
        /// <code>https://eastus.cognitiveservices.com/</code>
        /// </param>
        /// <param name="apiKey">The service key, copied from the Azure Portal.</param>
        /// <param name="options">General service options for the Form Recognizer client.</param>
        public FormRecognizerClient(Uri endpoint, string apiKey, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(endpoint, nameof(endpoint));
            Throw.IfMissing(apiKey, nameof(apiKey));
            Throw.IfNullOrEmpty(apiKey, nameof(apiKey));
            Throw.IfMissing(options, nameof(options));
            Throw.IfInvalidUri(endpoint, nameof(endpoint));
            _authentication = new FormHttpPolicy(endpoint, apiKey, options);
            var pipeline = HttpPipelineBuilder.Build(options, _authentication);

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
