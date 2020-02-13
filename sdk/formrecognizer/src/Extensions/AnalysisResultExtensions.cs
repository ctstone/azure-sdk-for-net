// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;

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
        /// <param name="analysis">Form analysis result.</param>
        public static TextExtractionPage[] GetRawTextExtraction(this AnalysisResult analysis)
        {
            return analysis.TextExtractionPages;
        }
    }
}