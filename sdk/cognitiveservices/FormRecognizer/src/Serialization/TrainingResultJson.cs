// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TrainingResultJson
    {
        public static TrainingResult Read(JsonElement root)
        {
            var trainingResult = TrainingResult.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref trainingResult, property);
            }
            return trainingResult;
        }

        private static void ReadPropertyValue(ref TrainingResult trainingResult, JsonProperty property)
        {
            if (property.NameEquals("trainingDocuments"))
            {
                trainingResult.TrainingDocuments = ArrayJson.Read(property.Value, TrainingDocumentJson.Read);
            }
            else if (property.NameEquals("fields"))
            {
                trainingResult.Fields = ArrayJson.Read(property.Value, TrainingFieldJson.Read);
            }
            else if (property.NameEquals("averageModelAccuracy"))
            {
                trainingResult.AverageModelAccuracy = property.Value.GetSingle();
            }
            else if (property.NameEquals("errors"))
            {
                trainingResult.Errors = ArrayJson.Read(property.Value, ErrorDetailsJson.Read);
            }
        }
    }
}