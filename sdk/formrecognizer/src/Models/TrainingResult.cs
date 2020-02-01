// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Custom model training result.
    /// </summary>
    internal class TrainingResult
    {
        // TODO: Does training documents apply to both supervised and unsupervised learning?

        /// <summary>
        /// List of the documents used to train the model and any errors reported in each document.
        /// </summary>
        public TrainingDocumentResult[] TrainingDocumentResults { get; internal set; }

        // TODO: Does Fields apply only to supervised models?  How is this different from FormClusters?

        /// <summary>
        /// List of fields used to train the model and the train operation error reported by each.
        /// </summary>
        public TrainingFieldAccuracy[] FieldAccuracies { get; internal set; }

        /// <summary>
        /// Average accuracy.
        /// </summary>
        public float? AverageModelAccuracy { get; internal set; }

        /// <summary>
        /// Errors returned during the training operation.
        /// </summary>
        public FormRecognizerError[] Errors { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingResult"/> class.
        /// </summary>
        protected TrainingResult() { }

        internal static TrainingResult Create() => new TrainingResult();
    }
}