// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TrainingFieldJson
    {
        public static TrainingField Read(JsonElement root)
        {
            var trainingField = TrainingField.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref trainingField, property);
                }
            }
            return trainingField;
        }

        private static void ReadPropertyValue(ref TrainingField trainingField, JsonProperty property)
        {
            if (property.NameEquals("fieldName"))
            {
                trainingField.Name = property.Value.GetString();
            }
            else if (property.NameEquals("accuracy"))
            {
                trainingField.Accuracy = property.Value.GetSingle();
            }
        }
    }
}