// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// A set of extracted fields corresponding to the input document.
    /// </summary>
    public class PredefinedForm
    {
        /// <summary>
        /// Document type.
        /// </summary>
        public string DocumentType { get; }

        /// <summary>
        /// Get the first page number where the document is found.
        /// </summary>
        public int FirstPageNumber { get; }

        /// <summary>
        /// Get the last page number where the document is found.
        /// </summary>
        public int LastPageNumber { get; }

        /// <summary>
        /// Dictionary of named field values.
        /// </summary>
        public IDictionary<string, PredefinedField> Fields { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredefinedFormInternal"/> class.
        /// </summary>
        protected PredefinedForm()
        { }

        internal PredefinedForm(PredefinedFormInternal field)
        {
            DocumentType = field.DocumentType;
            FirstPageNumber = field.FirstPageNumber;
            LastPageNumber = field.LastPageNumber;
            Fields = field.Fields;
        }
    }
}