// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Prediction
{
    // TODO: Solve referencing problem, wherein the table cell or other element
    // points back to the raw OCR TextElement.

    /// <summary>
    /// Information about the extracted cell in a table.
    /// </summary>
    public class ExtractedTableCell
    {
        /// <summary>
        /// The text content of the line.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Bounding box of the extracted line.
        /// </summary>
        public float[] BoundingBox { get; internal set; }

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
        public int RowSpan { get; internal set; }

        /// <summary>
        /// Number of columns spanned by this cell.
        /// </summary>
        public int ColumnSpan { get; internal set; }

        /// <summary>
        /// Is the current cell a header cell?
        /// </summary>
        public bool IsHeader { get; internal set; }

        /// <summary>
        /// Is the current cell a footer cell?
        /// </summary>
        public bool IsFooter { get; internal set; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float Confidence { get; internal set; }

        // TODO: What is this data table for?

        internal ExtractedTable DataTable { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedTableCell"/> class.
        /// </summary>
        protected ExtractedTableCell()
        { }

        internal static ExtractedTableCell Create() => new ExtractedTableCell();
    }
}