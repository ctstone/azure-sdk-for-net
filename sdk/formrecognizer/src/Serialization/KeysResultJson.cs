// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class KeysResultJson
    {
        public static KeysResult Read(JsonElement root)
        {
            var keysResult = KeysResult.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref keysResult, property);
            }
            if (keysResult.Clusters == default)
            {
                keysResult.Clusters = new Dictionary<string, string[]>();
            }
            return keysResult;
        }

        private static void ReadPropertyValue(ref KeysResult keysResult, JsonProperty property)
        {
            if (property.NameEquals("clusters"))
            {
                keysResult.Clusters = ObjectJson.Read(property.Value, ArrayJson.ReadStrings);
            }
        }
    }
}