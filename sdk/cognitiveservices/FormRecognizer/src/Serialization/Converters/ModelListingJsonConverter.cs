// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization.Converters
{
    internal class ModelListingJsonConverter : JsonConverter<ModelListing>
    {
        public override ModelListing Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument json = JsonDocument.ParseValue(ref reader);
            JsonElement root = json.RootElement;

            return Read(root);
        }

        public override void Write(Utf8JsonWriter writer, ModelListing value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public static ModelListing Read(JsonElement root)
        {
            var modelListing = ModelListing.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref modelListing, property);
            }
            return modelListing;
        }

        private static void ReadPropertyValue(ref ModelListing modelListing, JsonProperty property)
        {
            if (property.NameEquals("summary"))
            {
                modelListing.Summary = ModelsSummaryJson.Read(property.Value);
            }
            else if (property.NameEquals("modelList"))
            {
                modelListing.ModelList = ArrayJson.Read(property.Value, ModelInfoJson.Read);
            }
            else if (property.NameEquals("nextLink"))
            {
                modelListing.NextLink = property.Value.GetString();
            }
        }
    }
}