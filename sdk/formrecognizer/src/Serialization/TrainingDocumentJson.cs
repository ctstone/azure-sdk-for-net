// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class TrainingDocumentJson
    {
        public static TrainingDocument Read(JsonElement root)
        {
            var trainingDocument = TrainingDocument.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref trainingDocument, property);
            }
            if (trainingDocument.Errors == default)
            {
                trainingDocument.Errors = Array.Empty<ErrorDetails>();
            }
            return trainingDocument;
        }

        private static void ReadPropertyValue(ref TrainingDocument trainingDocument, JsonProperty property)
        {
            if (property.NameEquals("documentName"))
            {
                trainingDocument.DocumentName = property.Value.GetString();
            }
            else if (property.NameEquals("pages"))
            {
                trainingDocument.Pages = property.Value.GetInt32();
            }
            else if (property.NameEquals("errors"))
            {
                trainingDocument.Errors = ArrayJson.Read(property.Value, ErrorDetailsJson.Read);
            }
            else if (property.NameEquals("status"))
            {
                trainingDocument.Status = EnumJson.Read<TrainingStatus>(property.Value);
            }
        }
    }
}