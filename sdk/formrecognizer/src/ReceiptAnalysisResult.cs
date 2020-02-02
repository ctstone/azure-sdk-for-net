// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class ReceiptAnalysisResult
    {
        /// <summary>
        /// Status of the operation.
        /// </summary>
        internal AnalysisStatus Status { get; set; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        internal DateTimeOffset LastUpdateTime { get; set; }

        // TODO: do we need these timestamps or status for any customer scenario?

        /// <summary>
        /// Date and time when the analysis operation was submitted.
        /// </summary>
        internal DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// </summary>
        public ReceiptType ReceiptType { get; internal set; }

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

        /// <summary>
        /// Output of the Optical Character Recognition engine, including text
        /// elements with bounding boxes, as well as page geometry, and page and line languages.
        /// </summary>
        public OcrExtractedPage[] ExtractedPages { get; internal set; }
    }
}