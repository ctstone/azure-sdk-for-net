// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// A set of extracted fields corresponding to the input document.
    /// </summary>
    public class DocumentResult
    {
        /// <summary>
        /// Document type.
        /// </summary>
        public string DocumentType { get; internal set; }

        /// <summary>
        /// First and last page number where the document is found.
        /// </summary>
        public (int, int) PageRange { get; internal set; }

        /// <summary>
        /// Dictionary of named field values.
        /// </summary>
        public IDictionary<string, FieldValue> Fields { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentResult"/> class.
        /// </summary>
        protected DocumentResult()
        { }

        internal static DocumentResult Create() => new DocumentResult();
    }
}