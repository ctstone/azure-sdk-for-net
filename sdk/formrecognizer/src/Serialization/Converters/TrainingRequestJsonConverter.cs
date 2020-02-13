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
            if (value.Filter != default)
            {
                writer.WritePropertyName("sourceFilter");
                writer.WriteStartObject();
                if (value.Filter.Prefix != default)
                {
                    writer.WriteString("prefix", value.Filter.Prefix);
                }
                if (value.Filter.IncludeSubFolders.HasValue)
                {
                    writer.WriteBoolean("includeSubFolders", value.Filter.IncludeSubFolders.Value);
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