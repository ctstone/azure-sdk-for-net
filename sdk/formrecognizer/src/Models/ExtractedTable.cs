// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// Information about the extracted table contained in a page.
    /// </summary>
    public class ExtractedTable
    {
        /// <summary>
        /// Number of rows.
        /// </summary>
        public int Rows { get; internal set; }

        /// <summary>
        /// Number of columns.
        /// </summary>
        public int Columns { get; internal set; }

        /// <summary>
        /// List of cells contained in the table.
        /// </summary>
        public ExtractedTableCell[] Cells { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedTable"/> class.
        /// </summary>
        protected ExtractedTable()
        { }

        internal static ExtractedTable Create() => new ExtractedTable();
    }
}