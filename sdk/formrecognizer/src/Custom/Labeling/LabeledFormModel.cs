// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom.Labels
{

    /// <summary>
    /// A custom, labeled form model.
    /// </summary>
    public class LabeledFormModel
    {
        /// <summary>
        /// Get information about the current model.
        /// </summary>
        public ModelInfo Information { get; }

        /// <summary>
        /// Get training documents used to generate the current model.
        /// </summary>
        public TrainingDocument[] Documents { get; }

        /// <summary>
        /// Get training fields recognized by the current model.
        /// </summary>
        public TrainingField[] Fields { get; }

        /// <summary>
        /// Get training errors for the current model.
        /// </summary>
        public ErrorDetails[] Errors { get; }

        /// <summary>
        /// Get the average model accuracy.
        /// </summary>
        public float AverageAccuracy { get; }


        internal LabeledFormModel(CustomFormModel model)
        {
            Information = model.ModelInfo;
            Documents = model.TrainResult?.TrainingDocuments ?? Array.Empty<TrainingDocument>();
            Fields = model.TrainResult?.Fields ?? Array.Empty<TrainingField>();
            Errors = model.TrainResult.Errors ?? Array.Empty<ErrorDetails>();
            AverageAccuracy = model.TrainResult.AverageModelAccuracy.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabeledFormModel"/> class.
        /// </summary>
        protected LabeledFormModel()
        {
        }
    }
}