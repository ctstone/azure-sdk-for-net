// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// Data table.
    /// </summary>
    public class ClusteredDataTable : TableExtraction
    {
        /// <summary>
        /// Get the cluster identifier associated with the current field extraction.
        /// </summary>
        public int ClusterId { get; }

        internal ClusteredDataTable(FieldExtractionPageInternal page, TableExtractionInternal dataTable)
            : base(page, dataTable)
        {
            ClusterId = page.DocumentClusterId.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredDataTable"/> class.
        /// </summary>
        protected ClusteredDataTable()
        {
        }
    }
}