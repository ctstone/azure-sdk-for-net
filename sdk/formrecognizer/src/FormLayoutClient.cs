// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Http;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core;
using Azure.Core.Pipeline;

#pragma warning disable AZC0007 // Layout client shares options with FormRecognizerClient

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// The form layout client extracts text and layout information from a given document.
    /// </summary>
    public class FormLayoutClient : AnalyzeClient
    {
        private const string BaseLayoutPath = "/layout";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class using a key-based credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        public FormLayoutClient(Uri endpoint, CognitiveKeyCredential credential)
            : this(endpoint, credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class using a subscription key credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        /// <param name="options">Optional service parameters.</param>
        public FormLayoutClient(Uri endpoint, CognitiveKeyCredential credential, FormRecognizerClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class using an Azure Active Directory credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        public FormLayoutClient(Uri endpoint, TokenCredential credential)
            : this(endpoint, credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class using an Azure Active Directory credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        /// <param name="options">Optional service parameters.</param>
        public FormLayoutClient(Uri endpoint, TokenCredential credential, FormRecognizerClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class using a user-defined credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">User-defined credential.</param>
        public FormLayoutClient(Uri endpoint, CognitiveHeaderCredential credential)
            : this(endpoint, credential, new FormRecognizerClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class using a user-defined credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">User-defined credential</param>
        /// <param name="options">Optional service parameters.</param>
        public FormLayoutClient(Uri endpoint, CognitiveHeaderCredential credential, FormRecognizerClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        internal FormLayoutClient(Uri endpoint, FormAuthenticator authenticator, FormRecognizerClientOptions options)
            : base(FormHttpPipelineBuilder.Build(endpoint, authenticator, options), options, BaseLayoutPath)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class for mocking.
        /// </summary>
        protected FormLayoutClient()
        { }

        #endregion
    }

    /// <summary>
    /// Options for analyzing layout.
    /// </summary>
    public struct AnalyzeLayoutOptions
    { }
}