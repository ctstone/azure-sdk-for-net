// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Request parameter to train a new custom model.
    /// </summary>
    internal class TrainingRequest
    {
        /// <summary>
        /// Source path containing the training documents.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Filter to apply to the documents in the source path for training.
        /// </summary>
        public SourceFilter SourceFilter { get; set; }

        /// <summary>
        /// Use label file for training a model.
        /// </summary>
        public bool? UseLabelFile { get; set; }
    }
}