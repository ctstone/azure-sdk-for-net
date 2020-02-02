// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class DataTableJson
    {
        public static ExtractedTable Read(JsonElement root)
        {
            var dataTable = ExtractedTable.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref dataTable, property);
                }
            }

            if (dataTable.Cells == default)
            {
                dataTable.Cells = Array.Empty<ExtractedTableCell>();
            }
            return dataTable;
        }

        private static void ReadPropertyValue(ref ExtractedTable dataTable, JsonProperty property)
        {
            if (property.NameEquals("rows"))
            {
                dataTable.Rows = property.Value.GetInt32();
            }
            else if (property.NameEquals("columns"))
            {
                dataTable.Columns = property.Value.GetInt32();
            }
            else if (property.NameEquals("cells"))
            {
                dataTable.Cells = ArrayJson.Read(property.Value, DataTableCellJson.Read);
            }
        }
    }
}