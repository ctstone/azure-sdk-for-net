// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class FormAnalysis
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
        /// Get the schema version used for this result.
        /// </summary>
        /// <value></value>
        public string Version { get; }

        /// <summary>
        /// Get raw text extractions by page.
        /// </summary>
        /// <value></value>
        public TextExtractionPage[] TextExtractionPages { get; }

        /// <summary>
        /// Get all fields recognized in the current analysis.
        /// </summary>
        /// <value></value>
        public FieldExtraction[] Fields { get; }

        /// <summary>
        /// Get all tables recognized in the current analysis.
        /// </summary>
        public ClusteredDataTable[] Tables { get; }

        internal FormAnalysis(AnalysisInternal analysis)
        {
            var fieldExtractionPages = analysis.AnalyzeResult?.FieldExtractionPages ?? Array.Empty<FieldExtractionPageInternal>();
            Status = analysis.Status;
            CreatedOn = analysis.CreatedOn;
            LastUpdatedOn = analysis.LastUpdatedOn;
            Version = analysis.AnalyzeResult?.Version;
            TextExtractionPages = analysis.AnalyzeResult?.TextExtractionPages ?? Array.Empty<TextExtractionPage>();
            Fields = fieldExtractionPages
                .SelectMany((page) => page.Fields.Select((field) => (page, field)))
                .Select((x) => new FieldExtraction(x.page, x.field))
                .ToArray();
            Tables = fieldExtractionPages
                .SelectMany((page) => page.Tables.Select((table) => (page, table)))
                .Select((x) => new ClusteredDataTable(x.page, x.table))
                .ToArray() ?? Array.Empty<ClusteredDataTable>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAnalysis"/> class.
        /// </summary>
        protected FormAnalysis()
        {
        }
    }
}