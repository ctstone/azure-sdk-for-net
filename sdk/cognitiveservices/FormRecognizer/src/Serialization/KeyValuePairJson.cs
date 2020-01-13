// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class KeyValuePairJson
    {
        public static KeyValuePair Read(JsonElement root)
        {
            var keyValuePair = KeyValuePair.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref keyValuePair, property);
            }
            return keyValuePair;
        }

        private static void ReadPropertyValue(ref KeyValuePair keyValuePair, JsonProperty property)
        {
            if (property.NameEquals("label"))
            {
                keyValuePair.Label = property.Value.GetString();
            }
            else if (property.NameEquals("key"))
            {
                keyValuePair.Key = KeyValueElementJson.Read(property.Value);
            }
            else if (property.NameEquals("value"))
            {
                keyValuePair.Key = KeyValueElementJson.Read(property.Value);
            }
            else if (property.NameEquals("confidence"))
            {
                keyValuePair.Confidence = property.Value.GetSingle();
            }
        }
    }
}