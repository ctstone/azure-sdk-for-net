// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class FieldExtractionPageJson
    {
        public static FieldExtractionPageInternal Read(JsonElement root)
        {
            var pageResult = FieldExtractionPageInternal.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref pageResult, property);
                }
            }
            if (pageResult.Fields == default)
            {
                pageResult.Fields = Array.Empty<FieldExtractionInternal>();
            }
            if (pageResult.Tables == default)
            {
                pageResult.Tables = Array.Empty<DataTableInternal>();
            }
            return pageResult;
        }

        private static void ReadPropertyValue(ref FieldExtractionPageInternal fieldExtractionPage, JsonProperty property)
        {
            if (property.NameEquals("page"))
            {
                fieldExtractionPage.PageNumber = property.Value.GetInt32();
            }
            else if (property.NameEquals("clusterId"))
            {
                fieldExtractionPage.DocumentClusterId = property.Value.GetInt32();
            }
            else if (property.NameEquals("keyValuePairs"))
            {
                fieldExtractionPage.Fields = ArrayJson.Read(property.Value, FieldExtractionJson.Read);
            }
            else if (property.NameEquals("tables"))
            {
                fieldExtractionPage.Tables = ArrayJson.Read(property.Value, DataTableJson.Read);
            }
        }
    }
}