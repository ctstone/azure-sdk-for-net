// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ArrayJson
    {
        public static string[] ReadStrings(JsonElement root)
        {
            return Read(root, (json) => json.GetString());
        }
        public static float[] ReadSingles(JsonElement root)
        {
            return Read(root, (json) => json.GetSingle());
        }
        public static T[] Read<T>(JsonElement root, Func<JsonElement, T> factory)
        {
            var array = new T[root.GetArrayLength()];
            var i = 0;
            foreach (var json in root.EnumerateArray())
            {
                array[i] = factory(json);
                i += 1;
            }

            return array;
        }
    }
}