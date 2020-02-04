// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Serialization.Converters;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Custom Form Recognizer model.
    /// </summary>
    public class CustomFormModel
    {
        /// <summary>
        /// Basic custom model information.
        /// </summary>
        public ModelInfo ModelInfo { get; internal set; }

        /// <summary>
        /// Keys extracted by the custom model.
        /// </summary>
        public KeysResult Keys { get; internal set; }

        /// <summary>
        /// Custom model training result.
        /// </summary>
        public TrainingResult TrainResult { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormModel"/> class.
        /// </summary>
        protected CustomFormModel()
        { }

        internal static CustomFormModel Create() => new CustomFormModel();
    }
}