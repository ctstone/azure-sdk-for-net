// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Report for a custom model training field.
    /// </summary>
    internal class TrainingField
    {
        /// <summary>
        /// Training field name.
        /// </summary>
        public string FieldName { get; internal set; }

        /// <summary>
        /// Estimated extraction accuracy for this field.
        /// </summary>
        public float Accuracy { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingField"/> class.
        /// </summary>
        protected TrainingField()
        { }

        internal static TrainingField Create() => new TrainingField();
    }
}