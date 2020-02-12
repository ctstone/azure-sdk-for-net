// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Layout
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class LayoutAnalysis
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
        /// Get analysis processing time.
        /// </summary>
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

        /// <summary>
        /// Get all tables recognized in the current analysis.
        /// </summary>
        public DataTable[] Tables { get; }

        internal LayoutAnalysis(AnalysisInternal analysis)
        {
            var fieldExtractionPages = analysis.AnalyzeResult?.FieldExtractionPages ?? Array.Empty<FieldExtractionPageInternal>();
            Status = analysis.Status;
            CreatedOn = analysis.CreatedOn;
            LastUpdatedOn = analysis.LastUpdatedOn;
            Version = analysis.AnalyzeResult?.Version;
            TextExtractionPages = analysis.AnalyzeResult?.TextExtractionPages ?? Array.Empty<TextExtractionPage>();
            Tables = fieldExtractionPages
                .SelectMany((page) => page.Tables.Select((table) => (page, table)))
                .Select((x) => new DataTable(x.page, x.table))
                .ToArray() ?? Array.Empty<DataTable>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutAnalysis"/> class.
        /// </summary>
        protected LayoutAnalysis()
        {
        }
    }
}