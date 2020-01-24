// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

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
        private readonly CognitiveCredential _credential;

        /// <summary>
        /// Get the Cognitive Credential for this client.
        /// </summary>
        public virtual CognitiveCredential Credential => _credential;

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
        /// <param name="credential">A Cognitive Services credential object.</param>
        public FormRecognizerClient(CognitiveCredential credential)
        : this(credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class with options.
        /// </summary>
        /// <param name="credential">A Cognitive Services credential object.</param>
        /// <param name="options">Optional service parameters.</param>
        public FormRecognizerClient(CognitiveCredential credential, FormRecognizerClientOptions options)
        {
            Throw.IfMissing(credential, nameof(credential));
            Throw.IfMissing(options, nameof(options));
            _credential = credential;
            _authentication = new FormHttpPolicy(credential, options.Version);
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
