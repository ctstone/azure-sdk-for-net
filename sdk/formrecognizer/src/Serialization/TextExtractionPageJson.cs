// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TextExtractionPageJson
    {
        public static TextExtractionPage Read(JsonElement root)
        {
            var readResult = TextExtractionPage.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref readResult, property);
                }
            }
            if (readResult.Lines == default)
            {
                readResult.Lines = Array.Empty<TextLine>();
            }
            return readResult;
        }

        private static void ReadPropertyValue(ref TextExtractionPage textExtractionPage, JsonProperty property)
        {
            if (property.NameEquals("page"))
            {
                textExtractionPage.PageNumber = property.Value.GetInt32();
            }
            else if (property.NameEquals("angle"))
            {
                textExtractionPage.Angle = property.Value.GetSingle();
            }
            else if (property.NameEquals("width"))
            {
                textExtractionPage.Width = property.Value.GetSingle();
            }
            else if (property.NameEquals("height"))
            {
                textExtractionPage.Height = property.Value.GetSingle();
            }
            else if (property.NameEquals("unit"))
            {
                textExtractionPage.Unit = EnumJson.Read<PageUnit>(property.Value);
            }
            else if (property.NameEquals("language"))
            {
                textExtractionPage.Language = property.Value.GetString();
            }
            else if (property.NameEquals("lines"))
            {
                textExtractionPage.Lines = ArrayJson.Read(property.Value, TextLineJson.Read);
            }
        }
    }
}