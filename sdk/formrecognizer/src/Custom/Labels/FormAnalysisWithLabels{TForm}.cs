// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom.Labels
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class FormAnalysisWithLabels<TForm> : FormAnalysisWithLabels
        where TForm : new()
    {
        /// <summary>
        /// Get the predefined forms recognized in the current analysis.
        /// </summary>
        public PredefinedForm<TForm>[] Forms { get; }

        internal FormAnalysisWithLabels(AnalysisInternal analysis)
            : base(analysis)
        {
            var predefinedFields = analysis.AnalyzeResult?.PredefinedFieldExtractions ?? Array.Empty<PredefinedFormInternal>();
            Forms = predefinedFields
                .Select((x) => new PredefinedForm<TForm>(x))
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