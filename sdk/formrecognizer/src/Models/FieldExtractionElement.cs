// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted key or value in a key-value pair.
    /// </summary>
    public class FieldExtractionElementJson : ReferencingTextElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldExtractionElementJson"/> class.
        /// </summary>
        protected FieldExtractionElementJson()
        { }

        internal static FieldExtractionElementJson Create() => new FieldExtractionElementJson();
    }
}