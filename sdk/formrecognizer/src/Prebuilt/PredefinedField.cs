// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// A typed predefined field.
    /// </summary>
    public class PredefinedField<TValue> : ReferencingTextElement
    {
        /// <summary>
        /// Get the field value.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Get the field recognition confidence.
        /// </summary>
        public float? Confidence { get; }

        /// <summary>
        /// Get the page number where the field was recognized.
        /// </summary>
        public int? PageNumber { get; }

        internal PredefinedField(TValue value, PredefinedField field)
        {
            Value = value;
            Confidence = field?.Confidence;
            PageNumber = field?.PageNumber;
            Elements = field?.Elements ?? Array.Empty<TextElement>();
            Text = field?.Text;
            BoundingBox = field?.BoundingBox;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (Value != null)
            {
                return Value.ToString();
            }
            else if (Text != null)
            {
                return Text;
            }
            return string.Empty;
        }
    }
}