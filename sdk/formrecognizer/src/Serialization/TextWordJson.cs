// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Prediction;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TextWordJson
    {
        public static ExtractedWord Read(JsonElement root)
        {
            var textWord = ExtractedWord.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref textWord, property);
                }
            }
            return textWord;
        }

        private static void ReadPropertyValue(ref ExtractedWord textWord, JsonProperty property)
        {
            //if (property.NameEquals("text"))
            //{
            //    TextElementJson.ReadText(textWord, property.Value);
            //}
            //else if (property.NameEquals("boundingBox"))
            //{
            //    TextElementJson.ReadBoundingBox(textWord, property.Value);
            //}
            if (property.NameEquals("confidence"))
            {
                textWord.Confidence = property.Value.GetSingle();
            }
        }
    }
}