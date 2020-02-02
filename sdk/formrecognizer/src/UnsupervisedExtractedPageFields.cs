// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class UnsupervisedExtractedPageFields
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int PageNumber { get; internal set; }

        /// <summary>
        /// Cluster identifier.
        /// </summary>
        public int FormClusterId { get; internal set; }

        /// <summary>
        /// List of key-value pairs extracted from the page.
        /// </summary>
        public UnsupervisedExtractedField[] PageFields { get; internal set; }

        /// <summary>
        /// List of data tables extracted from the page.
        /// </summary>
        public ExtractedTable[] PageTables { get; internal set; }
    }
}