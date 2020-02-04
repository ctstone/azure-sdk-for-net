// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Extracted information from a single page.
    /// </summary>
    public class FieldExtractionPage
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int PageNumber { get; internal set; }

        /// <summary>
        /// Cluster identifier.
        /// </summary>
        public int? ClusterId { get; internal set; }

        /// <summary>
        /// List of key-value pairs extracted from the page.
        /// </summary>
        public FieldExtraction[] Fields { get; internal set; }

        /// <summary>
        /// List of data tables extracted from the page.
        /// </summary>
        public DataTable[] Tables { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldExtractionPage"/> class.
        /// </summary>
        protected FieldExtractionPage()
        { }

        internal static FieldExtractionPage Create() => new FieldExtractionPage();
    }
}