// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class ExtractedField
    {
        // TODO: What is the value of label if this is only in unsupervised learning?
        // Or is this used for supervised learning as well?

        /// <summary>
        /// A user defined label for the key/value pair entry.
        /// </summary>
        public string Label { get; internal set; }

        /// <summary>
        /// </summary>
        public ExtractedFieldName FieldName { get; internal set; }

        /// <summary>
        /// </summary>
        public ExtractedFieldValue FieldValue { get; internal set; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float Confidence { get; internal set; }
    }
}