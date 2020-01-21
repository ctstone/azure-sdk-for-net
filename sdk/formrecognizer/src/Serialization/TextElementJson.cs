// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TextElementJson
    {
        public static void ReadText(TextElement textElement, JsonElement root)
        {
            textElement.Text = root.GetString();
        }

        public static void ReadBoundingBox(TextElement textElement, JsonElement root)
        {
            textElement.BoundingBox = ArrayJson.ReadSingles(root);
        }
    }
}