// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Http;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core;
using Azure.AI.FormRecognizer.Layout;

#pragma warning disable AZC0007 // Client type should have public constructor with equivalent parameters not taking 'FormReceiptClientOptions' as last argument

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// The Form Layout client extracts text and layout information from documents.
    /// </summary>
    public class LayoutClient : AnalyzeClient<LayoutAnalysis>
    {
        private const string BaseLayoutPath = "/layout";

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutClient"/> class using a key-based credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        public LayoutClient(Uri endpoint, CognitiveKeyCredential credential)
            : this(endpoint, credential, new FormClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutClient"/> class using a subscription key credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        /// <param name="options">Optional service parameters.</param>
        public LayoutClient(Uri endpoint, CognitiveKeyCredential credential, FormClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutClient"/> class using an Azure Active Directory credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        public LayoutClient(Uri endpoint, TokenCredential credential)
            : this(endpoint, credential, new FormClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutClient"/> class using an Azure Active Directory credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Azure Active Directory credential.</param>
        /// <param name="options">Optional service parameters.</param>
        public LayoutClient(Uri endpoint, TokenCredential credential, FormClientOptions options)
            : this(endpoint, new FormAuthenticator(credential), options)
        {
        }

        internal LayoutClient(Uri endpoint, FormAuthenticator authenticator, FormClientOptions options)
            : base(FormHttpPipelineBuilder.Build(endpoint, authenticator, options), options.SerializationOptions, BaseLayoutPath, (model) => new LayoutAnalysis(model))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutClient"/> class for mocking.
        /// </summary>
        protected LayoutClient()
        { }

        #endregion
    }
}