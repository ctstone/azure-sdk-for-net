// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class FieldExtractionJson
    {
        public static FieldExtractionInternal Read(JsonElement root)
        {
            var keyValuePair = FieldExtractionInternal.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref keyValuePair, property);
                }
            }
            return keyValuePair;
        }

        private static void ReadPropertyValue(ref FieldExtractionInternal fieldExtraction, JsonProperty property)
        {
            if (property.NameEquals("label"))
            {
                fieldExtraction.Label = property.Value.GetString();
            }
            else if (property.NameEquals("key"))
            {
                fieldExtraction.Name = FieldExtractionElementJson.Read(property.Value);
            }
            else if (property.NameEquals("value"))
            {
                fieldExtraction.Value = FieldExtractionElementJson.Read(property.Value);
            }
            else if (property.NameEquals("confidence"))
            {
                fieldExtraction.Confidence = property.Value.GetSingle();
            }
        }
    }
}