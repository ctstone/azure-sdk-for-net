// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Extracted information from a single page.
    /// </summary>
    public class PageResult
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
        public DataTable[] Tables { get; internal set; }
    }
}