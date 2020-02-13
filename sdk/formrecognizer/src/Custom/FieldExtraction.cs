// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// Field extraction.
    /// </summary>
    public class FieldExtraction
    {
        /// <summary>
        /// Get the cluster identifier associated with the current field extraction.
        /// </summary>
        public int DocumentClusterId { get; }

        /// <summary>
        /// Get the page number where the current field was extracted.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Information about the extracted key in a key-value pair.
        /// </summary>
        public FieldExtractionElement Name { get; }

        /// <summary>
        /// Information about the extracted value in a key-value pair.
        /// </summary>
        public FieldExtractionElement Value { get; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float Confidence { get; }

        internal FieldExtraction(FieldExtractionPageInternal page, FieldExtractionInternal fieldExtraction)
        {
            DocumentClusterId = page.DocumentClusterId.Value;
            PageNumber = page.PageNumber;
            Name = fieldExtraction.Name;
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