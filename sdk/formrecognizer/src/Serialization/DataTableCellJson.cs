// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class DataTableCellJson
    {
        public static DataTableCell Read(JsonElement root)
        {
            var dataTableCell = DataTableCell.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref dataTableCell, property);
                }
            }
            return dataTableCell;
        }

        private static void ReadPropertyValue(ref DataTableCell dataTable, JsonProperty property)
        {
            if (property.NameEquals("rowIndex"))
            {
                dataTable.RowIndex = property.Value.GetInt32();
            }
            else if (property.NameEquals("columnIndex"))
            {
                dataTable.ColumnIndex = property.Value.GetInt32();
            }
            else if (property.NameEquals("rowSpan"))
            {
                dataTable.RowSpan = property.Value.GetInt32();
            }
            else if (property.NameEquals("columnSpan"))
            {
                dataTable.ColumnSpan = property.Value.GetInt32();
            }
            else if (property.NameEquals("elements"))
            {
                dataTable.ElementReferences = ArrayJson.ReadStrings(property.Value);
            }
            else if (property.NameEquals("isHeader"))
            {
                dataTable.IsHeader = property.Value.GetBoolean();
            }
            else if (property.NameEquals("isFooter"))
            {
                dataTable.IsFooter = property.Value.GetBoolean();
            }
            else if (property.NameEquals("confidence"))
            {
                dataTable.Confidence = property.Value.GetSingle();
            }
            else if (property.NameEquals("text"))
            {
                TextElementJson.ReadText(dataTable, property.Value);
            }
            else if (property.NameEquals("boundingBox"))
            {
                TextElementJson.ReadBoundingBox(dataTable, property.Value);
            }
        }
    }
}