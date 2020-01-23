// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ModelsSummaryJson
    {
        public static ModelsSummary Read(JsonElement root)
        {
            var modelsSummary = ModelsSummary.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref modelsSummary, property);
                }
            }
            return modelsSummary;
        }

        private static void ReadPropertyValue(ref ModelsSummary modelsSummary, JsonProperty property)
        {
            if (property.NameEquals("count"))
            {
                modelsSummary.Count = property.Value.GetInt32();
            }
            else if (property.NameEquals("limit"))
            {
                modelsSummary.Limit = property.Value.GetInt32();
            }
            else if (property.NameEquals("lastUpdatedDateTime"))
            {
                modelsSummary.LastUpdatedDateTime = property.Value.GetDateTimeOffset();
            }
        }
    }
}