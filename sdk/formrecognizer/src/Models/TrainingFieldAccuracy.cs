// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Report for a custom model training field.
    /// </summary>
    internal class TrainingFieldAccuracy
    {
        /// <summary>
        /// Training field name.
        /// </summary>
        public string FieldName { get; internal set; }


        // TODO: How is Accuracy different from Confidence?

        /// <summary>
        /// Estimated extraction accuracy for this field.
        /// </summary>
        public float Accuracy { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingFieldAccuracy"/> class.
        /// </summary>
        protected TrainingFieldAccuracy()
        { }

        internal static TrainingFieldAccuracy Create() => new TrainingFieldAccuracy();
    }
}