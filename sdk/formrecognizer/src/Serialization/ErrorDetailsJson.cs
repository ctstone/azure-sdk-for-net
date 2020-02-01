// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ErrorDetailsJson
    {
        public static FormRecognizerError Read(JsonElement root)
        {
            var dataTable = new FormRecognizerError();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref dataTable, property);
                }
            }
            return dataTable;
        }

        private static void ReadPropertyValue(ref FormRecognizerError errorDetails, JsonProperty property)
        {
            if (property.NameEquals("code"))
            {
                errorDetails.Code = property.Value.GetString();
            }
            else if (property.NameEquals("message"))
            {
                errorDetails.Message = property.Value.GetString();
            }
        }
    }
}