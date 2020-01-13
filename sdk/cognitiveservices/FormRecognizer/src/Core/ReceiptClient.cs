// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Class to analyze receipts.
    /// </summary>
    public class ReceiptClient : PrebuiltFormClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptClient"/> class.
        /// </summary>
        protected ReceiptClient()
        { }

        internal ReceiptClient(HttpPipeline pipeline, FormRecognizerClientOptions options)
            : base("receipt", pipeline, options)
        { }
    }
}