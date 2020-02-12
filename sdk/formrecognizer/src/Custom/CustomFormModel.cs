// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom
{

    /// <summary>
    /// A custom form model.
    /// </summary>
    public class CustomFormModel
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
        /// Get a mapping of recognized document clusters and their respective keys.
        /// </summary>
        public IDictionary<string, string[]> DocumentKeyClusters { get; }

        /// <summary>
        /// Get training errors for the current model.
        /// </summary>
        public ErrorDetails[] Errors { get; }

        internal CustomFormModel(CustomFormModelInternal model)
        {
            Information = model.ModelInfo;
            Documents = model.TrainResult?.TrainingDocuments ?? Array.Empty<TrainingDocument>();
            DocumentKeyClusters = model.Keys.Clusters ?? new Dictionary<string, string[]>();
            Errors = model.TrainResult.Errors ?? Array.Empty<ErrorDetails>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormModel"/> class.
        /// </summary>
        protected CustomFormModel()
        {
        }

        /// <inheritdoc />
        public override string ToString() => Information.ToString();
    }
}