// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ObjectJson
    {
        public static IDictionary<string, T> Read<T>(JsonElement root, Func<JsonElement, T> factory)
        {
            var dictionary = new Dictionary<string, T>();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (var json in root.EnumerateObject())
                {
                    dictionary[json.Name] = factory(json.Value);
                }
            }
            return dictionary;
        }
    }
}