// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TableCellExtractionJson
    {
        public static TableCellExtraction Read(JsonElement root)
        {
            var dataTableCell = TableCellExtraction.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref dataTableCell, property);
                }
            }
            if (!dataTableCell.ColumnSpan.HasValue)
            {
                dataTableCell.ColumnSpan = 1;
            }
            if (!dataTableCell.RowSpan.HasValue)
            {
                dataTableCell.RowSpan = 1;
            }
            return dataTableCell;
        }

        private static void ReadPropertyValue(ref TableCellExtraction cell, JsonProperty property)
        {
            if (property.NameEquals("rowIndex"))
            {
                cell.RowIndex = property.Value.GetInt32();
            }
            else if (property.NameEquals("columnIndex"))
            {
                cell.ColumnIndex = property.Value.GetInt32();
            }
            else if (property.NameEquals("rowSpan"))
            {
                cell.RowSpan = property.Value.GetInt32();
            }
            else if (property.NameEquals("columnSpan"))
            {
                cell.ColumnSpan = property.Value.GetInt32();
            }
            else if (property.NameEquals("elements"))
            {
                cell.ElementReferences = ArrayJson.ReadStrings(property.Value);
            }
            else if (property.NameEquals("isHeader"))
            {
                cell.IsHeader = property.Value.GetBoolean();
            }
            else if (property.NameEquals("isFooter"))
            {
                cell.IsFooter = property.Value.GetBoolean();
            }
            else if (property.NameEquals("confidence"))
            {
                cell.Confidence = property.Value.GetSingle();
            }
            else if (property.NameEquals("text"))
            {
                TextElementJson.ReadText(cell, property.Value);
            }
            else if (property.NameEquals("boundingBox"))
            {
                TextElementJson.ReadBoundingBox(cell, property.Value);
            }
        }
    }
}