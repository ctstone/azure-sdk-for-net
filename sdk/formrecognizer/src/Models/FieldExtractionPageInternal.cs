// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Extracted information from a single page.
    /// </summary>
    internal class FieldExtractionPageInternal
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int PageNumber { get; internal set; }

        /// <summary>
        /// Cluster identifier.
        /// </summary>
        public int? DocumentClusterId { get; internal set; }

        /// <summary>
        /// List of key-value pairs extracted from the page.
        /// </summary>
        public FieldExtractionInternal[] Fields { get; internal set; }

        /// <summary>
        /// List of data tables extracted from the page.
        /// </summary>
        public DataTableInternal[] Tables { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldExtractionPageInternal"/> class.
        /// </summary>
        protected FieldExtractionPageInternal()
        { }

        internal static FieldExtractionPageInternal Create() => new FieldExtractionPageInternal();
    }
}