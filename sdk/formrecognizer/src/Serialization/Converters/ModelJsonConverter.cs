// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;

namespace Azure.AI.FormRecognizer.Serialization.Converters
{
    internal class ModelJsonConverter : JsonConverter<UnsupervisedModelTrainingResult>
    {
        public override UnsupervisedModelTrainingResult Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument json = JsonDocument.ParseValue(ref reader);
            JsonElement root = json.RootElement;

            return Read(root);
        }

        public override void Write(Utf8JsonWriter writer, UnsupervisedModelTrainingResult value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public static UnsupervisedModelTrainingResult Read(JsonElement root)
        {
            var model = UnsupervisedModelTrainingResult.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    //ReadPropertyValue(ref model, property);
                }
            }
            return model;
        }

        //private static void ReadPropertyValue(ref UnsupervisedModelTrainingResult model, JsonProperty property)
        //{
        //    throw new NotImplementedException();
        //    //if (property.NameEquals("modelInfo"))
        //    //{
        //    //    model.ModelInfo = ModelInfoJson.Read(property.Value);
        //    //}
        //    //else if (property.NameEquals("keys"))
        //    //{
        //    //    model.Keys = KeysResultJson.Read(property.Value);
        //    //}
        //    //else if (property.NameEquals("trainResult"))
        //    //{
        //    //    model.TrainResult = TrainingResultJson.Read(property.Value);
        //    //}
        //}
    }
}