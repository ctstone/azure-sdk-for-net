// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TextWordJson
    {
        public static TextWord Read(JsonElement root)
        {
            var textWord = TextWord.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref textWord, property);
                }
            }
            return textWord;
        }

        private static void ReadPropertyValue(ref TextWord textWord, JsonProperty property)
        {
            if (property.NameEquals("text"))
            {
                TextElementJson.ReadText(textWord, property.Value);
            }
            else if (property.NameEquals("boundingBox"))
            {
                TextElementJson.ReadBoundingBox(textWord, property.Value);
            }
            else if (property.NameEquals("confidence"))
            {
                textWord.Confidence = property.Value.GetSingle();
            }
        }
    }
}