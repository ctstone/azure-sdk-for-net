// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class ExtractedReceipt
    {
        /// <summary>
        /// </summary>
        public ReceiptType ReceiptType { get; internal set; }

        // TODO: It would be better to have these properties be strongly typed
        // rather than being a predefined field value.  But they need the confidence
        // values attached!  Think more about this.

        /// <summary>
        /// </summary>
        public PredefinedFieldValue MerchantName { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue MerchantAddress { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue MerchantPhoneNumber { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue TransactionDate { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue TransactionTime { get; internal set; }

        /// <summary>
        /// </summary>
        public ReceiptItem[] Items { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue Subtotal { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue Tax { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue Tip { get; internal set; }

        /// <summary>
        /// </summary>
        public PredefinedFieldValue Total { get; internal set; }
    }
}
