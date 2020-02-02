// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;
using System;
using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// A set of predefined fields extracted from a form by a supervised learning model.
    /// </summary>
    public class ExtractedForm
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
        /// </summary>
        public PredefinedFieldCollection Fields { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedForm"/> class.
        /// </summary>
        protected ExtractedForm()
        {
        }

        internal static ExtractedForm Create() => new ExtractedForm();
    }
}