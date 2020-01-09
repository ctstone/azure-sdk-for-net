// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Operations;
using Azure.AI.FormRecognizer.Pipeline;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Hello World
    /// </summary>
    public class FormRecognizerClient
    {
        private readonly CustomFormClient _customFormClient;
        private readonly ReceiptClient _receiptClient;
        private readonly FormLayoutClient _layoutClient;
        private readonly ApimAuthenticationPolicy _authentication;

        /// <summary>
        /// Interact with custom Form Recognizer models.
        /// </summary>
        public CustomFormClient Custom => _customFormClient;

        /// <summary>
        /// Receipts.
        /// </summary>
        public ReceiptClient Receipt => _receiptClient;

        /// <summary>
        /// Layout.
        /// </summary>
        public FormLayoutClient Layout => _layoutClient;

        /// <summary>
        /// Api key.
        /// </summary>
        public string ApiKey
        {
            get { return _authentication.ApiKey; }
            set { _authentication.ApiKey = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="apiKey"></param>
        public FormRecognizerClient(string endpoint, string apiKey)
        : this(endpoint, apiKey, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="apiKey"></param>
        /// <param name="options"></param>
        public FormRecognizerClient(string endpoint, string apiKey, FormRecognizerClientOptions options)
        {
            ThrowIfMissing(endpoint, nameof(endpoint));
            ThrowIfMissing(apiKey, nameof(apiKey));
            ThrowIfMissing(options, nameof(options));
            _authentication = new ApimAuthenticationPolicy(new Uri(endpoint), apiKey, options);
            var pipeline = HttpPipelineBuilder.Build(options, _authentication);

            _customFormClient = new CustomFormClient(pipeline, options);
            _receiptClient = new ReceiptClient(pipeline);
            _layoutClient = new FormLayoutClient(pipeline);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class.
        /// </summary>
        protected FormRecognizerClient()
        {
        }

        private static void ThrowIfMissing<T>(T arg, string name)
        {
            if (arg.Equals(default(T)))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
