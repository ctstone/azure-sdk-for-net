// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Custom model training result.
    /// </summary>
    public class TrainingResult
    {
        /// <summary>
        /// List of the documents used to train the model and any errors reported in each document.
        /// </summary>
        public TrainingDocument[] TrainingDocuments { get; internal set; }

        /// <summary>
        /// List of fields used to train the model and the train operation error reported by each.
        /// </summary>
        public TrainingField[] Fields { get; internal set; }

        /// <summary>
        /// Average accuracy.
        /// </summary>
        public float? AverageModelAccuracy { get; internal set; }

        /// <summary>
        /// Errors returned during the training operation.
        /// </summary>
        public ErrorDetails[] Errors { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingResult"/> class.
        /// </summary>
        protected TrainingResult() { }

        internal static TrainingResult Create() => new TrainingResult();
    }
}