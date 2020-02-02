// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Prediction;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Extracted information from a single page.
    /// </summary>
    internal class PageResult
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int Page { get; internal set; }

        /// <summary>
        /// Cluster identifier.
        /// </summary>
        public int? ClusterId { get; internal set; }

        /// <summary>
        /// List of key-value pairs extracted from the page.
        /// </summary>
        public KeyValuePair[] KeyValuePairs { get; internal set; }

        /// <summary>
        /// List of data tables extracted from the page.
        /// </summary>
        public ExtractedTable[] PageTables { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageResult"/> class.
        /// </summary>
        protected PageResult()
        { }

        internal static PageResult Create() => new PageResult();
    }
}