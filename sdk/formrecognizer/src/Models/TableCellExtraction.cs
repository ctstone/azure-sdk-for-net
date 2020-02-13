// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted cell in a table.
    /// </summary>
    public class TableCellExtraction : ReferencingTextElement
    {
        /// <summary>
        /// Row index of the cell.
        /// </summary>
        public int RowIndex { get; internal set; }

        /// <summary>
        /// Column index of the cell.
        /// </summary>
        public int ColumnIndex { get; internal set; }

        /// <summary>
        /// Number of rows spanned by this cell.
        /// </summary>
        public int? RowSpan { get; internal set; }

        /// <summary>
        /// Number of columns spanned by this cell.
        /// </summary>
        public int? ColumnSpan { get; internal set; }

        /// <summary>
        /// Is the current cell a header cell?
        /// </summary>
        public bool? IsHeader { get; internal set; }

        /// <summary>
        /// Is the current cell a footer cell?
        /// </summary>
        public bool? IsFooter { get; internal set; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float Confidence { get; internal set; }

        internal TableExtractionInternal DataTable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableCellExtraction"/> class.
        /// </summary>
        protected TableCellExtraction()
        { }

        internal static TableCellExtraction Create() => new TableCellExtraction();
    }
}