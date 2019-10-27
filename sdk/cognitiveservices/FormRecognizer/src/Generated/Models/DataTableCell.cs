// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Information about the extracted cell in a table.
    /// </summary>
    public partial class DataTableCell
    {
        /// <summary>
        /// Initializes a new instance of the DataTableCell class.
        /// </summary>
        public DataTableCell()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DataTableCell class.
        /// </summary>
        /// <param name="rowIndex">Row index of the cell.</param>
        /// <param name="columnIndex">Column index of the cell.</param>
        /// <param name="text">Text content of the cell.</param>
        /// <param name="boundingBox">Bounding box of the cell.</param>
        /// <param name="confidence">Confidence value.</param>
        /// <param name="rowSpan">Number of rows spanned by this cell.</param>
        /// <param name="columnSpan">Number of columns spanned by this
        /// cell.</param>
        /// <param name="elements">When includeTextDetails is set to true, a
        /// list of references to the text elements constituting this table
        /// cell.</param>
        /// <param name="isHeader">Is the current cell a header cell?</param>
        /// <param name="isFooter">Is the current cell a footer cell?</param>
        public DataTableCell(int rowIndex, int columnIndex, string text, IList<double> boundingBox, double confidence, int rowSpan = default(int), int columnSpan = default(int), IList<string> elements = default(IList<string>), bool isHeader = default(bool), bool isFooter = default(bool))
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            RowSpan = rowSpan;
            ColumnSpan = columnSpan;
            Text = text;
            BoundingBox = boundingBox;
            Confidence = confidence;
            Elements = elements;
            IsHeader = isHeader;
            IsFooter = isFooter;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets row index of the cell.
        /// </summary>
        [JsonProperty(PropertyName = "rowIndex")]
        public int RowIndex { get; set; }

        /// <summary>
        /// Gets or sets column index of the cell.
        /// </summary>
        [JsonProperty(PropertyName = "columnIndex")]
        public int ColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets number of rows spanned by this cell.
        /// </summary>
        [JsonProperty(PropertyName = "rowSpan")]
        public int RowSpan { get; set; }

        /// <summary>
        /// Gets or sets number of columns spanned by this cell.
        /// </summary>
        [JsonProperty(PropertyName = "columnSpan")]
        public int ColumnSpan { get; set; }

        /// <summary>
        /// Gets or sets text content of the cell.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets bounding box of the cell.
        /// </summary>
        [JsonProperty(PropertyName = "boundingBox")]
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets confidence value.
        /// </summary>
        [JsonProperty(PropertyName = "confidence")]
        public double Confidence { get; set; }

        /// <summary>
        /// Gets or sets when includeTextDetails is set to true, a list of
        /// references to the text elements constituting this table cell.
        /// </summary>
        [JsonProperty(PropertyName = "elements")]
        public IList<string> Elements { get; set; }

        /// <summary>
        /// Gets or sets is the current cell a header cell?
        /// </summary>
        [JsonProperty(PropertyName = "isHeader")]
        public bool IsHeader { get; set; }

        /// <summary>
        /// Gets or sets is the current cell a footer cell?
        /// </summary>
        [JsonProperty(PropertyName = "isFooter")]
        public bool IsFooter { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Text == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Text");
            }
            if (BoundingBox == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "BoundingBox");
            }
            if (RowIndex < 0)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "RowIndex", 0);
            }
            if (ColumnIndex < 0)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "ColumnIndex", 0);
            }
            if (RowSpan < 1)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "RowSpan", 1);
            }
            if (ColumnSpan < 1)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "ColumnSpan", 1);
            }
        }
    }
}
