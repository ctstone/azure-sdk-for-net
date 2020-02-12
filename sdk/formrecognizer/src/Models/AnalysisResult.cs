// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Base class for analysis.
    /// </summary>
    public abstract class AnalysisResult
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
        /// Get the time spent to analyze the request.
        /// </summary>
        /// <value></value>
        public TimeSpan Duration => LastUpdatedOn - CreatedOn;

        /// <summary>
        /// Get the schema version used for this result.
        /// </summary>
        /// <value></value>
        public string Version { get; }

        /// <summary>
        /// Get raw text extractions by page.
        /// </summary>
        /// <value></value>
        internal TextExtractionPage[] TextExtractionPages { get; }

        internal AnalysisResult(AnalysisInternal analysis)
        {
            Status = analysis.Status;
            CreatedOn = analysis.CreatedOn;
            LastUpdatedOn = analysis.LastUpdatedOn;
            Version = analysis.AnalyzeResult?.Version;
            TextExtractionPages = analysis.AnalyzeResult?.TextExtractionPages ?? Array.Empty<TextExtractionPage>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisResult"/> class.
        /// </summary>
        protected AnalysisResult()
        {
        }
    }
}