// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class DataTableJson
    {
        public static DataTable Read(JsonElement root)
        {
            var dataTable = DataTable.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref dataTable, property);
            }
            return dataTable;
        }

        private static void ReadPropertyValue(ref DataTable dataTable, JsonProperty property)
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