// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ReadResultJson
    {
        public static ReadResult Read(JsonElement root)
        {
            var readResult = ReadResult.Create();
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

        private static void ReadPropertyValue(ref ReadResult readResult, JsonProperty property)
        {
            if (property.NameEquals("page"))
            {
                readResult.Page = property.Value.GetInt32();
            }
            else if (property.NameEquals("angle"))
            {
                readResult.Angle = property.Value.GetSingle();
            }
            else if (property.NameEquals("width"))
            {
                readResult.Width = property.Value.GetSingle();
            }
            else if (property.NameEquals("height"))
            {
                readResult.Height = property.Value.GetSingle();
            }
            else if (property.NameEquals("unit"))
            {
                readResult.Unit = EnumJson.Read<Unit>(property.Value);
            }
            else if (property.NameEquals("language"))
            {
                readResult.Language = EnumJson.Read<Language>(property.Value);
            }
            else if (property.NameEquals("lines"))
            {
                readResult.Lines = ArrayJson.Read(property.Value, TextLineJson.Read);
            }
        }
    }
}