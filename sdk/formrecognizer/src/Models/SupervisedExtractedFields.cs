// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// A set of extracted fields corresponding to the input document.
    /// </summary>
    public class SupervisedExtractedFields
    {
        // TODO: Possible values include, receipt, layout, user-specified?
        /// <summary>
        /// Document type.
        /// </summary>
        public string FormType { get; internal set; }

        /// <summary>
        /// First and last page number where the form is found within the input file.
        /// </summary>
        public (int, int) FormPageRange { get; internal set; }

        /// <summary>
        /// Dictionary of named field values, where the key is the name of the label
        /// for the form field specified during training time, and the FieldValue is the value
        /// read from the form field.
        /// </summary>
        public IDictionary<string, CategorizedFieldValue> FormFields { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SupervisedExtractedFields"/> class.
        /// </summary>
        protected SupervisedExtractedFields()
        {
        }

        internal static SupervisedExtractedFields Create() => new SupervisedExtractedFields();
    }
}