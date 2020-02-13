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
    public class ReceiptAnalysis : AnalysisResult
    {
        /// <summary>
        /// Get all recognized receipts from the current analysis.
        /// </summary>
        /// <value></value>
        public ReceiptExtraction[] Receipts { get; }

        internal ReceiptAnalysis(AnalysisInternal analysis)
            : base(analysis)
        {
            var predefinedFieldExtractions = analysis.AnalyzeResult?.PredefinedFieldExtractions ?? Array.Empty<PredefinedFormInternal>();
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