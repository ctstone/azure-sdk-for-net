// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Custom;
using Azure.AI.FormRecognizer.Custom.Labels;
using Azure.AI.FormRecognizer.Layout;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prebuilt;

namespace Azure.AI.FormRecognizer.TextExtraction
{
    /// <summary>
    /// Extensions for analysis results.
    /// </summary>
    public static class AnalysisResultExtensions
    {
        /// <summary>
        /// Get raw text extraction results.
        /// </summary>
        /// <param name="analysis">Form analysis.</param>
        public static TextExtractionPage[] GetRawTextExtraction(this FormAnalysis analysis)
        {
            return analysis.TextExtractionPages;
        }

        /// <summary>
        /// Get raw text extraction results.
        /// </summary>
        /// <param name="analysis">Form analysis.</param>
        public static TextExtractionPage[] GetRawTextExtraction(this FormAnalysisWithLabels analysis)
        {
            return analysis.TextExtractionPages;
        }

        /// <summary>
        /// Get raw text extraction results.
        /// </summary>
        /// <param name="analysis">Form analysis.</param>
        public static TextExtractionPage[] GetRawTextExtraction(this LayoutAnalysis analysis)
        {
            return analysis.TextExtractionPages;
        }

        /// <summary>
        /// Get raw text extraction results.
        /// </summary>
        /// <param name="analysis">Form analysis.</param>
        public static TextExtractionPage[] GetRawTextExtraction(this ReceiptAnalysis analysis)
        {
            return analysis.TextExtractionPages;
        }
    }
}