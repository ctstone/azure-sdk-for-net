// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Recognized field value.
    /// </summary>
    public class PredefinedField : ReferencingTextElement
    {
        /// <summary>
        /// Type of field value.
        /// </summary>
        public PredefinedFieldType Type { get; internal set; }

        /// <summary>
        /// String value.
        /// </summary>
        public string StringValue { get; internal set; }

        /// <summary>
        /// Date value.
        /// </summary>
        public DateTimeOffset DateValue { get; internal set; }

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
        public PredefinedField[] ArrayValue { get; internal set; }

        /// <summary>
        /// Dictionary of field values.
        /// </summary>
        public IDictionary<string, PredefinedField> ObjectValue { get; internal set; }

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
        public int? PageNumber { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredefinedField"/> class.
        /// </summary>
        protected PredefinedField()
        { }

        internal static PredefinedField Create() => new PredefinedField();
    }
}