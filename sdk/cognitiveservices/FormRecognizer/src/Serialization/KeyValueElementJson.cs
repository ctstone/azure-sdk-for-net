// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class KeyValueElementJson
    {
        public static KeyValueElement Read(JsonElement root)
        {
            var keyValueElement = KeyValueElement.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref keyValueElement, property);
            }
            return keyValueElement;
        }

        private static void ReadPropertyValue(ref KeyValueElement keyValuePair, JsonProperty property)
        {
            if (property.NameEquals("text"))
            {
                TextElementJson.ReadText(keyValuePair, property.Value);
            }
            else if (property.NameEquals("boundingBox"))
            {
                TextElementJson.ReadBoundingBox(keyValuePair, property.Value);
            }
            else if (property.NameEquals("elements"))
            {
                keyValuePair.ElementReferences = ArrayJson.ReadStrings(property.Value);
            }
        }
    }
}