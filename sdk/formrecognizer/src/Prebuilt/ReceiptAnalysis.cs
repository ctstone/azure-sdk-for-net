// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Prebuilt
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class ReceiptAnalysis
    {
        /// <summary>
        /// Status of the operation.
        /// </summary>
        public OperationStatus Status { get; }

        /// <summary>
        /// Date and time when the analysis operation was submitted.
        /// </summary>
        public DateTimeOffset CreatedOn { get; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedOn { get; }

        /// <summary>
        /// Get the analysis duration time.
        /// </summary>
        public TimeSpan Duration => LastUpdatedOn - CreatedOn;

        /// <summary>
        /// Get the schema version used for the current analysis.
        /// </summary>
        /// <value></value>
        public string Version { get; }

        /// <summary>
        /// Get raw text extractions by page.
        /// </summary>
        /// <value></value>
        public TextExtractionPage[] TextExtractionPages { get; }

        /// <summary>
        /// Get all recognized receipts from the current analysis.
        /// </summary>
        /// <value></value>
        public ReceiptExtraction[] Receipts { get; }

        internal ReceiptAnalysis(AnalysisInternal analysis)
        {
            var predefinedFieldExtractions = analysis.AnalyzeResult?.PredefinedFieldExtractions ?? Array.Empty<PredefinedFormInternal>();
            Status = analysis.Status;
            CreatedOn = analysis.CreatedOn;
            LastUpdatedOn = analysis.LastUpdatedOn;
            Version = analysis.AnalyzeResult?.Version;
            TextExtractionPages = analysis.AnalyzeResult?.TextExtractionPages ?? Array.Empty<TextExtractionPage>();
            Receipts = predefinedFieldExtractions
                .Select((x) => new ReceiptExtraction(x.Fields))
                .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReceiptAnalysis"/> class.
        /// </summary>
        protected ReceiptAnalysis()
        {
        }
    }
}