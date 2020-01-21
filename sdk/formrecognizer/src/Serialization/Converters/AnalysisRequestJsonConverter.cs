// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization.Converters
{
    internal class AnalysisRequestJsonConverter : JsonConverter<AnalysisRequest>
    {
        public override AnalysisRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, AnalysisRequest value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("source", value.Source);
            writer.WriteEndObject();
        }
    }
}