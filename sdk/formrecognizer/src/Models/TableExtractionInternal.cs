// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted table contained in a page.
    /// </summary>
    internal class TableExtractionInternal
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
        public TableCellExtraction[] Cells { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableExtractionInternal"/> class.
        /// </summary>
        protected TableExtractionInternal()
        { }

        internal static TableExtractionInternal Create() => new TableExtractionInternal();
    }
}