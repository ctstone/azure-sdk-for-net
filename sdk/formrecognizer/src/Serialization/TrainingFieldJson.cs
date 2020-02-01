// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TrainingFieldJson
    {
        public static FieldAccuracy Read(JsonElement root)
        {
            var trainingField = FieldAccuracy.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref trainingField, property);
                }
            }
            return trainingField;
        }

        private static void ReadPropertyValue(ref FieldAccuracy trainingField, JsonProperty property)
        {
            if (property.NameEquals("fieldName"))
            {
                trainingField.FieldName = property.Value.GetString();
            }
            else if (property.NameEquals("accuracy"))
            {
                trainingField.Accuracy = property.Value.GetSingle();
            }
        }
    }
}