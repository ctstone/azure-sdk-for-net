// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization.Converters
{
    internal class ErrorResponseJsonConverter : JsonConverter<ErrorResponse>
    {
        public override ErrorResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument json = JsonDocument.ParseValue(ref reader);
            JsonElement root = json.RootElement;

            return Read(root);
        }

        public override void Write(Utf8JsonWriter writer, ErrorResponse value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public static ErrorResponse Read(JsonElement root)
        {
            var errorResponse = new ErrorResponse();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref errorResponse, property);
                }
            }
            return errorResponse;
        }

        private static void ReadPropertyValue(ref ErrorResponse errorResponse, JsonProperty property)
        {
            if (property.NameEquals("error"))
            {
                errorResponse.Error = ErrorDetailsJson.Read(property.Value);
            }
        }
    }
}