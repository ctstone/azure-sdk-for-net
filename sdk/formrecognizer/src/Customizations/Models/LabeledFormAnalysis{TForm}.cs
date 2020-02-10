// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class LabeledFormAnalysis<TForm> : LabeledFormAnalysis
        where TForm : new()
    {
        /// <summary>
        /// Get the predefined forms recognized in the current analysis.
        /// </summary>
        public PredefinedForm<TForm>[] Forms { get; }

        internal LabeledFormAnalysis(AnalysisInternal analysis)
            : base(analysis)
        {
            var predefinedFields = analysis.AnalyzeResult?.PredefinedFieldExtractions ?? Array.Empty<PredefinedFormInternal>();
            Forms = predefinedFields
                .Select((x) => new PredefinedForm<TForm>(x))
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