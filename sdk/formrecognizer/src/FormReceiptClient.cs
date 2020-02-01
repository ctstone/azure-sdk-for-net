// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Http;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// The receipt client extract field text and semantic values from receipt documents.
    /// </summary>
    internal class FormReceiptClient : AnalyzeClient
    {
        internal const string BaseReceiptPath = "/prebuilt/receipt";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class using a key-based credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        public FormReceiptClient(Uri endpoint, CognitiveKeyCredential credential)
            : this(endpoint, credential, new FormReceiptClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class using a subscription key credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        /// <param name="options">Optional service parameters.</param>
        public FormReceiptClient(Uri endpoint, CognitiveKeyCredential credential, FormReceiptClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class using an Azure Active Directory credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        public FormReceiptClient(Uri endpoint, TokenCredential credential)
            : this(endpoint, credential, new FormReceiptClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class using an Azure Active Directory credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        /// <param name="options">Optional service parameters.</param>
        public FormReceiptClient(Uri endpoint, TokenCredential credential, FormReceiptClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class using a user-defined credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">User-defined credential.</param>
        public FormReceiptClient(Uri endpoint, CognitiveHeaderCredential credential)
            : this(endpoint, credential, new FormReceiptClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class using a user-defined credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">User-defined credential</param>
        /// <param name="options">Optional service parameters.</param>
        public FormReceiptClient(Uri endpoint, CognitiveHeaderCredential credential, FormReceiptClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        internal FormReceiptClient(Uri endpoint, FormAuthenticator authenticator, FormReceiptClientOptions options)
            : base(FormHttpPipelineBuilder.Build(endpoint, authenticator, options), options.SerializationOptions, BaseReceiptPath)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormReceiptClient"/> class for mocking.
        /// </summary>
        protected FormReceiptClient()
        { }

        #endregion
    }
}