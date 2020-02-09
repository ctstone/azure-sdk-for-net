// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Data table.
    /// </summary>
    public class ClusteredDataTable : DataTable
    {
        /// <summary>
        /// Get the cluster identifier associated with the current field extraction.
        /// </summary>
        public int ClusterId { get; }

        internal ClusteredDataTable(FieldExtractionPageInternal page, DataTableInternal dataTable)
            : base(page, dataTable)
        {
            ClusterId = page.ClusterId.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClusteredDataTable"/> class.
        /// </summary>
        protected ClusteredDataTable()
        {
        }
    }
}