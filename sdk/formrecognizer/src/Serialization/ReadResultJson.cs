// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Prediction;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ReadResultJson
    {
        public static OcrExtractedPage Read(JsonElement root)
        {
            var readResult = OcrExtractedPage.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref readResult, property);
                }
            }
            if (readResult.Lines == default)
            {
                readResult.Lines = Array.Empty<ExtractedLine>();
            }
            return readResult;
        }

        private static void ReadPropertyValue(ref OcrExtractedPage readResult, JsonProperty property)
        {
            if (property.NameEquals("page"))
            {
                readResult.PageNumber = property.Value.GetInt32();
            }
            else if (property.NameEquals("angle"))
            {
                readResult.TextAngle = property.Value.GetSingle();
            }
            else if (property.NameEquals("width"))
            {
                readResult.PageWidth = property.Value.GetSingle();
            }
            else if (property.NameEquals("height"))
            {
                readResult.PageHeight = property.Value.GetSingle();
            }
            else if (property.NameEquals("unit"))
            {
                readResult.Unit = EnumJson.Read<FormGeometryUnit>(property.Value);
            }
            else if (property.NameEquals("language"))
            {
                readResult.Language = property.Value.GetString();
            }
            //else if (property.NameEquals("lines"))
            //{
            //    readResult.Lines = ArrayJson.Read(property.Value, TextLineJson.Read);
            //}
        }
    }
}