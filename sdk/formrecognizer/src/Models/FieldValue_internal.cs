// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Prediction;
using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Recognized field value.
    /// </summary>
    internal class FieldValue_internal : ReferencingTextElement
    {
        /// <summary>
        /// Type of field value.
        /// </summary>
        public FieldValueType Type { get; internal set; }

        /// <summary>
        /// String value.
        /// </summary>
        public string StringValue { get; internal set; }

        /// <summary>
        /// Date value.
        /// </summary>
        public string DateValue { get; internal set; }

        /// <summary>
        /// Time value.
        /// </summary>
        public string TimeValue { get; internal set; }

        /// <summary>
        /// Phone number value.
        /// </summary>
        public string PhoneNumberValue { get; internal set; }

        /// <summary>
        /// Floating point value.
        /// </summary>
        public float? NumberValue { get; internal set; }

        /// <summary>
        /// Integer value.
        /// </summary>
        public int? IntegerValue { get; internal set; }

        /// <summary>
        /// Array of field values.
        /// </summary>
        public FieldValue_internal[] ArrayValue { get; internal set; }

        /// <summary>
        /// Dictionary of field values.
        /// </summary>
        public IDictionary<string, FieldValue_internal> ObjectValue { get; internal set; }

        /// <summary>
        /// When includeTextDetails is set to true, a list of references to the text elements constituting this field.
        /// </summary>

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float? Confidence { get; internal set; }

        /// <summary>
        /// The 1-based page number in the input document.
        /// </summary>
        public int? Page { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldValue_internal"/> class.
        /// </summary>
        protected FieldValue_internal()
        { }

        internal static FieldValue_internal Create() => new FieldValue_internal();
    }
}