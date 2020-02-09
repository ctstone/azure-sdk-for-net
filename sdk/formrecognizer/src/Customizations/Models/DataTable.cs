// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Data table.
    /// </summary>
    public class DataTable
    {
        /// <summary>
        /// Get the page number where the current field was extracted.
        /// </summary>
        public int PageNumber { get; }

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
        public DataTableCell[] Cells { get; internal set; }

        internal DataTable(FieldExtractionPageInternal page, DataTableInternal dataTable)
        {
            PageNumber = page.PageNumber;
            Rows = dataTable.Rows;
            Columns = dataTable.Columns;
            Cells = dataTable.Cells;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataTable"/> class.
        /// </summary>
        protected DataTable()
        {
        }

        /// <summary>
        /// Write an ASCII-formatted table to a <see cref="TextWriter" />.
        ///
        /// ```csharp
        /// table.WriteAscii(Console.Out);
        /// ```
        /// </summary>
        /// <param name="writer">A TextWriter that will write the results (e.g. `Console.Out`).</param>
        /// <param name="unicode">Use extended unicode box characters.</param>
        /// <param name="cellWidth">Maximum cell width in characters.</param>
        public void WriteAscii(TextWriter writer, bool unicode = true, int cellWidth = 15)
        {
            var topLeft = unicode ? '┌' : '|';
            var topRight = unicode ? '┐' : '|';
            var topInner = unicode ? '┬' : '|';
            var bottomLeft = unicode ? '└' : '|';
            var bottomRight = unicode ? '┘' : '|';
            var bottomInner = unicode ? '┴' : '|';
            var innerLeft = unicode ? '├' : '|';
            var innerRight = unicode ? '┤' : '|';
            var innerCross = unicode ? '┼' : '|';
            var innerHeaderLeft = unicode ? '╞' : '|';
            var innerHeaderRight = unicode ? '╡' : '|';
            var innerHeaderCross = unicode ? '╪' : '|';
            var inner = unicode ? '│' : '|';
            var lineSeparator = unicode ? '─' : '-';
            var headerSeparator = unicode ? '═' : '=';
            var footerSeparator = '~';
            var index = IndexCells();

            // write table top border
            for (var colIndex = 0; colIndex < Columns; colIndex += 1)
            {
                var firstCol = colIndex == 0;
                var line = new string(lineSeparator, cellWidth);
                var boundary = firstCol ? topLeft : topInner;
                writer.Write($"{boundary}{line}");
            }
            writer.WriteLine($"{topRight}");

            // write table
            for (var rowIndex = 0; rowIndex < Rows; rowIndex += 1)
            {
                var headerColumns = new bool[Columns];
                var footerColumns = new bool[Columns];
                var lastRow = rowIndex == Rows - 1;

                // write row
                for (var colIndex = 0; colIndex < Columns; colIndex += 1)
                {
                    var firstCol = colIndex == 0;
                    var lastCol = colIndex == Columns - 1;
                    if (index.TryGetValue(rowIndex, out IDictionary<int, DataTableCell> row))
                    {
                        if (row.TryGetValue(colIndex, out DataTableCell cell))
                        {
                            var colspan = cell.ColumnSpan ?? 1;
                            var maxWidth = cellWidth * colspan; // TODO
                            var text = cell.Text.Substring(0, Math.Min(cell.Text.Length, maxWidth));
                            writer.Write($"{inner}{{0, {cellWidth}}}", text);
                            for (var i = cell.ColumnIndex; i < cell.ColumnIndex + cell.ColumnSpan; i += 1)
                            {
                                headerColumns[i] = cell.IsHeader ?? false;
                                footerColumns[i] = cell.IsFooter ?? false;
                            }
                            colIndex += colspan - 1;
                        }
                    }
                }

                // write row bottom border
                writer.WriteLine(inner);
                for (var colIndex = 0; colIndex < Columns; colIndex += 1)
                {
                    var firstCol = colIndex == 0;
                    var isHeader = headerColumns[colIndex];
                    var isFooter = footerColumns[colIndex];
                    var lineChar = isHeader ? headerSeparator : isFooter ? footerSeparator : lineSeparator;
                    var line = new string(lineChar, cellWidth);
                    char boundary;
                    if (lastRow)
                    {
                        if (firstCol)
                        {
                            boundary = bottomLeft;
                        }
                        else
                        {
                            boundary = bottomInner;
                        }
                    }
                    else if (firstCol)
                    {
                        boundary = isHeader ? innerHeaderLeft : innerLeft;
                    }
                    else
                    {
                        boundary = isHeader ? innerHeaderCross : innerCross;
                    }
                    writer.Write($"{boundary}{line}");
                }
                writer.WriteLine(lastRow ? bottomRight : headerColumns[Columns - 1] ? innerHeaderRight : innerRight);
            }
        }

        /// <summary>
        /// Write an Html-formatted table to a <see cref="TextWriter" />.
        ///
        /// ```csharp
        /// table.WriteHtml(Console.Out);
        /// ```
        /// </summary>
        /// <param name="writer">A TextWriter that will write the results (e.g. `Console.Out`).</param>
        /// <param name="className">The HTML class name that will be assigned to the table.</param>
        public void WriteHtml(TextWriter writer, string className = "azure-ai-formrecognizer")
        {
            var index = IndexCells();
            writer.WriteLine($"<table class=\"{className}\">");
            for (var rowIndex = 0; rowIndex < Rows; rowIndex += 1)
            {
                if (index.TryGetValue(rowIndex, out IDictionary<int, DataTableCell> row))
                {
                    var isRowHeader = row.Values.Any((x) => x.IsHeader ?? false);
                    var isRowFooter = row.Values.Any((x) => x.IsFooter ?? false);

                    // write thead/tfoot start
                    if (isRowHeader)
                    {
                        writer.WriteLine("<thead>");
                    }
                    else if (isRowFooter)
                    {
                        writer.WriteLine("<tfoot>");
                    }

                    // write row start
                    writer.WriteLine("<tr>");

                    // write cells
                    for (var colIndex = 0; colIndex < Columns; colIndex += 1)
                    {
                        if (row.TryGetValue(colIndex, out DataTableCell cell))
                        {
                            var tag = cell.IsHeader ?? false ? "th" : "td";
                            var cellFormat = @"<{0} colspan=""{2}"" rowspan=""{3}"">{1}</{0}>";
                            writer.WriteLine(cellFormat, tag, cell.Text, cell.ColumnSpan, cell.RowSpan);
                        }
                    }

                    // write row end
                    writer.WriteLine("</tr>");

                    // write thead/tfoot end
                    if (isRowHeader)
                    {
                        writer.WriteLine("</thead>");
                    }
                    else if (isRowFooter)
                    {
                        writer.WriteLine("</tfoot>");
                    }
                }
            }
            writer.WriteLine("</table>");
        }

        /// <summary>
        /// Write a Markdown-formatted table to a <see cref="TextWriter" />.
        /// </summary>
        /// <param name="writer"></param>
        public void WriteMarkdown(TextWriter writer)
        {
            var index = IndexCells();
            var columnWidths = IndexColumnWidths();
            var boundary = '|';
            var header = '-';
            for (var rowIndex = 0; rowIndex < Rows; rowIndex += 1)
            {
                if (index.TryGetValue(rowIndex, out IDictionary<int, DataTableCell> row))
                {
                    var isRowHeader = row.Values.Any((x) => x.IsHeader ?? false);
                    for (var colIndex = 0; colIndex < Columns; colIndex += 1)
                    {
                        if (row.TryGetValue(colIndex, out DataTableCell cell))
                        {
                            var columnWidth = columnWidths[colIndex];
                            writer.Write($"{boundary} {{0, {columnWidth}}} ", cell.Text);
                        }
                    }
                    writer.WriteLine(boundary);

                    if (isRowHeader)
                    {
                        for (var colIndex = 0; colIndex < Columns; colIndex += 1)
                        {
                            var columnWidth = columnWidths[colIndex];
                            var headerLine = new string(header, columnWidth);
                            writer.Write($"{boundary} {headerLine} ");
                        }
                        writer.WriteLine(boundary);
                    }
                }
            }
        }

        /// <summary>
        /// Get an ASCII-formatted table string.
        /// </summary>
        public override string ToString()
        {
            using var writer = new StringWriter();
            WriteAscii(writer);
            writer.Flush();
            return writer.ToString();
        }

        /// <summary>
        /// Get an HTML-formatted table string.
        /// </summary>
        public string ToHtmlString()
        {
            using var writer = new StringWriter();
            WriteHtml(writer);
            writer.Flush();
            return writer.ToString();
        }

        /// <summary>
        /// Get a Markdown-formatted table string.
        /// </summary>
        public string ToMarkdownString()
        {
            using var writer = new StringWriter();
            WriteMarkdown(writer);
            writer.Flush();
            return writer.ToString();
        }


        private Dictionary<int, IDictionary<int, DataTableCell>> IndexCells()
        {
            var index = new Dictionary<int, IDictionary<int, DataTableCell>>();
            foreach (var cell in Cells)
            {
                for (var rowIndex = cell.RowIndex; rowIndex < cell.RowIndex + cell.RowSpan; rowIndex += 1)
                {
                    if (!index.TryGetValue(rowIndex, out IDictionary<int, DataTableCell> row))
                    {
                        index[rowIndex] = row = new Dictionary<int, DataTableCell>();
                    }
                    row[cell.ColumnIndex] = cell;
                }
            }
            return index;
        }

        private Dictionary<int, int> IndexColumnWidths()
        {
            var index = new Dictionary<int, int>();
            foreach (var cell in Cells)
            {
                for (var colIndex = cell.ColumnIndex; colIndex < cell.ColumnIndex + cell.ColumnSpan; colIndex += 1)
                {
                    if (!index.TryGetValue(colIndex, out int maxWidth))
                    {
                        maxWidth = 0;
                    }
                    index[cell.ColumnIndex] = Math.Max(maxWidth, cell.Text.Length);
                }
            }
            return index;
        }
    }
}