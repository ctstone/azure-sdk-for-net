// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class FormAnalysis : AnalysisResult
    {
        /// <summary>
        /// Get all fields recognized in the current analysis.
        /// </summary>
        /// <value></value>
        public FieldExtraction[] Fields { get; }

        /// <summary>
        /// Get all tables recognized in the current analysis.
        /// </summary>
        public TableExtractionClustered[] Tables { get; }

        internal FormAnalysis(AnalysisInternal analysis)
            : base(analysis)
        {
            var fieldExtractionPages = analysis.AnalyzeResult?.FieldExtractionPages ?? Array.Empty<FieldExtractionPageInternal>();

            Fields = fieldExtractionPages
                .SelectMany((page) => page.Fields.Select((field) => (page, field)))
                .Select((x) => new FieldExtraction(x.page, x.field))
                .ToArray();
            Tables = fieldExtractionPages
                .SelectMany((page) => page.Tables.Select((table) => (page, table)))
                .Select((x) => new TableExtractionClustered(x.page, x.table))
                .ToArray() ?? Array.Empty<TableExtractionClustered>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAnalysis"/> class.
        /// </summary>
        protected FormAnalysis()
        {
        }
    }
}