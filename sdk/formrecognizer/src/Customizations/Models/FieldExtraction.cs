// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Field extraction.
    /// </summary>
    public class FieldExtraction
    {
        /// <summary>
        /// Get the cluster identifier associated with the current field extraction.
        /// </summary>
        public int ClusterId { get; }

        /// <summary>
        /// Get the page number where the current field was extracted.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// A user defined label for the key/value pair entry.
        /// </summary>
        public string Label { get; internal set; }

        /// <summary>
        /// Information about the extracted key in a key-value pair.
        /// </summary>
        public FieldExtractionElement Field { get; internal set; }

        /// <summary>
        /// Information about the extracted value in a key-value pair.
        /// </summary>
        public FieldExtractionElement Value { get; internal set; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float Confidence { get; internal set; }

        internal FieldExtraction(FieldExtractionPageInternal page, FieldExtractionInternal fieldExtraction)
        {
            ClusterId = page.ClusterId.Value;
            PageNumber = page.PageNumber;
            Label = fieldExtraction.Label;
            Field = fieldExtraction.Field;
            Value = fieldExtraction.Value;
            Confidence = fieldExtraction.Confidence;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldExtraction"/> class.
        /// </summary>
        protected FieldExtraction()
        {
        }
    }
}