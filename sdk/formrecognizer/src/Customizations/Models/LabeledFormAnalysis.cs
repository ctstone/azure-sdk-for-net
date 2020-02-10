// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class LabeledFormAnalysis
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
        public TextExtractionPage[] TextExtractionPages { get; }

        /// <summary>
        /// Get all tables recognized in the current analysis.
        /// </summary>
        public DataTable[] Tables { get; }

        /// <summary>
        /// Get the predefined form fields recognized in the current analysis.
        /// </summary>
        /// <value></value>
        public PredefinedForm[] FormFields { get; }

        internal LabeledFormAnalysis(AnalysisInternal analysis)
        {
            var fieldExtractionPages = analysis.AnalyzeResult?.FieldExtractionPages ?? Array.Empty<FieldExtractionPageInternal>();
            var predefinedFields = analysis.AnalyzeResult?.PredefinedFieldExtractions ?? Array.Empty<PredefinedFormInternal>();
            Status = analysis.Status;
            CreatedOn = analysis.CreatedOn;
            LastUpdatedOn = analysis.LastUpdatedOn;
            Version = analysis.AnalyzeResult?.Version;
            TextExtractionPages = analysis.AnalyzeResult?.TextExtractionPages ?? Array.Empty<TextExtractionPage>();
            Tables = fieldExtractionPages
                .SelectMany((page) => page.Tables.Select((table) => (page, table)))
                .Select((x) => new DataTable(x.page, x.table))
                .ToArray();
            FormFields = predefinedFields
                .Select((x) => new PredefinedForm(x))
                .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabeledFormAnalysis"/> class.
        /// </summary>
        protected LabeledFormAnalysis()
        {
        }
    }
}