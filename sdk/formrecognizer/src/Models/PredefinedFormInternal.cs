// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

// TODO: expose strong typing for Fields dictionary.
namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// A set of extracted fields corresponding to the input document.
    /// </summary>
    internal class PredefinedFormInternal
    {
        /// <summary>
        /// Document type.
        /// </summary>
        public string DocumentType { get; internal set; }

        /// <summary>
        /// Get the first page number where the document is found.
        /// </summary>
        public int FirstPageNumber { get; internal set; }

        /// <summary>
        /// Get the last page number where the document is found.
        /// </summary>
        public int LastPageNumber { get; internal set; }

        /// <summary>
        /// Dictionary of named field values.
        /// </summary>
        public IDictionary<string, PredefinedField> Fields { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredefinedFormInternal"/> class.
        /// </summary>
        protected PredefinedFormInternal()
        { }

        internal static PredefinedFormInternal Create() => new PredefinedFormInternal();
    }
}