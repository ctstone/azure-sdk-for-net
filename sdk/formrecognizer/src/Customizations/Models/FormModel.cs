// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{

    /// <summary>
    /// A custom form model.
    /// </summary>
    public class FormModel
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
        /// Get a mapping of recognized documents and their respective keys.
        /// </summary>
        public IDictionary<string, string[]> DocumentKeyClusters { get; }

        /// <summary>
        /// Get training errors for the current model.
        /// </summary>
        public ErrorDetails[] Errors { get; }

        internal FormModel(CustomFormModel model)
        {
            Information = model.ModelInfo;
            Documents = model.TrainResult?.TrainingDocuments ?? Array.Empty<TrainingDocument>();
            DocumentKeyClusters = model.Keys.Clusters ?? new Dictionary<string, string[]>();
            Errors = model.TrainResult.Errors ?? Array.Empty<ErrorDetails>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LabeledFormModel"/> class.
        /// </summary>
        protected FormModel()
        {
        }

        /// <inheritdoc />
        public override string ToString() => Information.ToString();
    }
}