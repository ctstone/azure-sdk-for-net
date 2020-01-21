// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization.Converters
{
    internal class TrainingRequestJsonConverter : JsonConverter<TrainingRequest>
    {
        public override TrainingRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, TrainingRequest value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("source", value.Source);
            if (value.SourceFilter != default)
            {
                writer.WritePropertyName("sourceFilter");
                writer.WriteStartObject();
                if (value.SourceFilter.Prefix != default)
                {
                    writer.WriteString("prefix", value.SourceFilter.Prefix);
                }
                if (value.SourceFilter.IncludeSubFolders.HasValue)
                {
                    writer.WriteBoolean("includeSubFolders", value.SourceFilter.IncludeSubFolders.Value);
                }
                writer.WriteEndObject();
            }
            if (value.UseLabelFile.HasValue)
            {
                writer.WriteBoolean("useLabelFile", value.UseLabelFile.Value);
            }
            writer.WriteEndObject();
        }
    }
}