// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class AnalysisResultJsonConverter : JsonConverter<AnalysisResult>
    {
        public override AnalysisResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument json = JsonDocument.ParseValue(ref reader);
            JsonElement root = json.RootElement;

            return ReadAnalysisResult(root);
        }

        public override void Write(Utf8JsonWriter writer, AnalysisResult value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        internal static AnalysisResult ReadAnalysisResult(JsonElement root)
        {
            var analysisResult = new AnalysisResult();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                // ReadPropertyValue(ref analysisResult, property);
            }
            return analysisResult;
        }

        // private static void ReadPropertyValue(ref AnalysisResult analyzedForm, JsonProperty property)
        // {

        // }
    }
}