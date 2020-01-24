// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization.Converters
{
    internal class AnalysisJsonConverter : JsonConverter<Analysis>
    {
        public override Analysis Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument json = JsonDocument.ParseValue(ref reader);
            JsonElement root = json.RootElement;

            return ReadAnalyzedForm(root);
        }

        public override void Write(Utf8JsonWriter writer, Analysis value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public static Analysis ReadAnalyzedForm(JsonElement root)
        {
            var analyzedForm = Analysis.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref analyzedForm, property);
                }
            }

            return analyzedForm;
        }

        private static void ReadPropertyValue(ref Analysis analyzedForm, JsonProperty property)
        {
            if (property.NameEquals("status"))
            {
                analyzedForm.Status = (OperationStatus)Enum.Parse(typeof(OperationStatus), property.Value.GetString(), ignoreCase: true);
            }
            else if (property.NameEquals("createdDateTime"))
            {
                analyzedForm.CreatedOn = property.Value.GetDateTimeOffset();
            }
            else if (property.NameEquals("lastUpdatedDateTime"))
            {
                analyzedForm.LastUpdatedOn = property.Value.GetDateTimeOffset();
            }
            else if (property.NameEquals("analyzeResult"))
            {
                analyzedForm.AnalyzeResult = AnalysisResultJson.Read(property.Value);
            }
        }
    }
}