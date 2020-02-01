// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Serialization.Converters;

namespace Azure.AI.FormRecognizer.Models
{
    // TODO: Should this be split into supervised and unsupervised custom models?

    /// <summary>
    /// Custom Form Recognizer model.
    /// </summary>
    public class FormRecognizerCustomModel
    {
        /// <summary>
        /// Model Id.
        /// </summary>
        public string ModelId { get; internal set; }

        /// <summary>
        /// Date and time when the model was created.
        /// </summary>
        public DateTimeOffset CreatedOn { get; internal set; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedOn { get; internal set; }

        /// <summary>
        /// Keys extracted by the custom model.
        /// </summary>
        // TODO: Question - will this be populated for supervised models?
        // If not, we should probably break FRCustomModel into supervised and unsupervised custom models.s
        public ICollection<FormCluster> FormClusters { get; internal set; }

        /// <summary>
        /// Custom model training result.
        /// </summary>
        public TrainingResult TrainResult { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerCustomModel"/> class.
        /// </summary>
        protected FormRecognizerCustomModel()
        { }

        internal static FormRecognizerCustomModel Create() => new FormRecognizerCustomModel();
    }
}