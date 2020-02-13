// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class ErrorDetailsJson
    {
        public static ErrorDetails Read(JsonElement root)
        {
            var errorDetails = new ErrorDetails();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref errorDetails, property);
                }
            }
            return errorDetails;
        }

        private static void ReadPropertyValue(ref ErrorDetails errorDetails, JsonProperty property)
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