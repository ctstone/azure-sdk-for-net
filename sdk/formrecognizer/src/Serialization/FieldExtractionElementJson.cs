// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class FieldExtractionElementJson
    {
        public static FieldExtractionElement Read(JsonElement root)
        {
            var keyValueElement = FieldExtractionElement.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref keyValueElement, property);
                }
            }
            if (keyValueElement.ElementReferences == default)
            {
                keyValueElement.ElementReferences = Array.Empty<string>();
            }
            return keyValueElement;
        }

        private static void ReadPropertyValue(ref FieldExtractionElement fieldExtractionElement, JsonProperty property)
        {
            if (property.NameEquals("text"))
            {
                TextElementJson.ReadText(fieldExtractionElement, property.Value);
            }
            else if (property.NameEquals("boundingBox"))
            {
                TextElementJson.ReadBoundingBox(fieldExtractionElement, property.Value);
            }
            else if (property.NameEquals("elements"))
            {
                fieldExtractionElement.ElementReferences = ArrayJson.ReadStrings(property.Value);
            }
        }
    }
}