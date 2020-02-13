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
    public class LayoutAnalysis : AnalysisResult
    {
        /// <summary>
        /// Get all tables recognized in the current analysis.
        /// </summary>
        public TableExtraction[] Tables { get; }

        internal LayoutAnalysis(AnalysisInternal analysis)
            : base(analysis)
        {
            var fieldExtractionPages = analysis.AnalyzeResult?.FieldExtractionPages ?? Array.Empty<FieldExtractionPageInternal>();
            Tables = fieldExtractionPages
                .SelectMany((page) => page.Tables.Select((table) => (page, table)))
                .Select((x) => new TableExtraction(x.page, x.table))
                .ToArray() ?? Array.Empty<TableExtraction>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutAnalysis"/> class.
        /// </summary>
        protected LayoutAnalysis()
        {
        }
    }
}