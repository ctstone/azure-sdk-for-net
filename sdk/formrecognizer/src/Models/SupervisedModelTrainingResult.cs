// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Serialization.Converters;

namespace Azure.AI.FormRecognizer.Training
{
    /// <summary>
    /// Custom Form Recognizer model.
    /// </summary>
    public class SupervisedModelTrainingResult
    {
        /// <summary>
        /// Model Id.
        /// </summary>
        public string ModelId { get; internal set; }

        /// <summary>
        /// Model status.
        /// </summary>
        internal ModelStatus Status { get; set; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        internal DateTimeOffset LastUpdateTime { get; set; }

        /// <summary>
        /// Date and time when the model was created.
        /// </summary>
        public DateTimeOffset CreationTime { get; internal set; }

        /// <summary>
        /// List of the documents used to train the model and any errors reported in each document.
        /// </summary>
        public DocumentTrainingResult[] DocumentTrainingResults { get; internal set; }

        // TODO: Do field accuracies apply only to supervised models?  How is this different from FormClusters?

        /// <summary>
        /// List of fields used to train the model and the train operation error reported by each.
        /// </summary>
        public FieldAccuracy[] FieldAccuracies { get; internal set; }

        /// <summary>
        /// Average accuracy.
        /// </summary>
        public float? AverageModelAccuracy { get; internal set; }

        /// <summary>
        /// Errors returned during the training operation.
        /// </summary>
        public FormRecognizerError[] Errors { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupervisedModelTrainingResult"/> class.
        /// </summary>
        protected SupervisedModelTrainingResult()
        { }

        internal static SupervisedModelTrainingResult Create() => new SupervisedModelTrainingResult();
    }
}