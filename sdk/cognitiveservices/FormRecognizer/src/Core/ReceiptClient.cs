// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// The receipt client extract field text and semantic values from receipt documents.
    /// </summary>
    public class ReceiptClient : PrebuiltFormClient
    {
        private const string ModelName = "receipt";

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptClient"/> class.
        /// </summary>
        protected ReceiptClient()
        { }

        internal ReceiptClient(HttpPipeline pipeline, FormRecognizerClientOptions options)
            : base(ModelName, pipeline, options)
        { }
    }
}