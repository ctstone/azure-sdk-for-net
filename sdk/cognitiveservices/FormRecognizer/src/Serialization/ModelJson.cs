// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ModelJson
    {
        public static Model Read(JsonElement root)
        {
            var model = Model.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref model, property);
            }
            return model;
        }

        private static void ReadPropertyValue(ref Model model, JsonProperty property)
        {
            if (property.NameEquals("modelInfo"))
            {
                model.ModelInfo = ModelInfoJson.Read(property.Value);
            }
            else if (property.NameEquals("keys"))
            {
                model.Keys = KeysResultJson.Read(property.Value);
            }
            else if (property.NameEquals("trainResult"))
            {
                model.TrainResult = TrainingResultJson.Read(property.Value);
            }
        }
    }
}