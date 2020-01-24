// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ModelInfoJson
    {
        public static ModelInfo Read(JsonElement root)
        {
            var modelInfo = ModelInfo.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref modelInfo, property);
                }
            }
            return modelInfo;
        }

        private static void ReadPropertyValue(ref ModelInfo modelInfo, JsonProperty property)
        {
            if (property.NameEquals("modelId"))
            {
                modelInfo.ModelId = property.Value.GetString();
            }
            else if (property.NameEquals("status"))
            {
                modelInfo.Status = EnumJson.Read<ModelStatus>(property.Value);
            }
            else if (property.NameEquals("createdDateTime"))
            {
                modelInfo.CreatedOn = property.Value.GetDateTimeOffset();
            }
            else if (property.NameEquals("lastUpdatedDateTime"))
            {
                modelInfo.LastUpdatedOn = property.Value.GetDateTimeOffset();
            }
        }
    }
}