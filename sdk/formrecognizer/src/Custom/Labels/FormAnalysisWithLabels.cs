// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prebuilt;

namespace Azure.AI.FormRecognizer.Custom.Labels
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class FormAnalysisWithLabels : AnalysisResult
    {
        /// <summary>
        /// Get all tables recognized in the current analysis.
        /// </summary>
        public TableExtraction[] Tables { get; }

        /// <summary>
        /// Get the predefined form field groups recognized in the current analysis.
        /// </summary>
        /// <value></value>
        public PredefinedForm[] FieldGroups { get; }

        internal FormAnalysisWithLabels(AnalysisInternal analysis)
            : base(analysis)
        {
            var fieldExtractionPages = analysis.AnalyzeResult?.FieldExtractionPages ?? Array.Empty<FieldExtractionPageInternal>();
            var predefinedFields = analysis.AnalyzeResult?.PredefinedFieldExtractions ?? Array.Empty<PredefinedFormInternal>();
            Tables = fieldExtractionPages
                .SelectMany((page) => page.Tables.Select((table) => (page, table)))
                .Select((x) => new TableExtraction(x.page, x.table))
                .ToArray();
            FieldGroups = predefinedFields
                .Select((x) => new PredefinedForm(x))
                .ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormAnalysisWithLabels"/> class.
        /// </summary>
        protected FormAnalysisWithLabels()
        {
        }
    }
}