// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class PredefinedFieldExtractionJson
    {
        public static PredefinedFieldExtractionInternal Read(JsonElement root)
        {
            var documentResult = PredefinedFieldExtractionInternal.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref documentResult, property);
                }
            }
            if (documentResult.Fields == default)
            {
                documentResult.Fields = new Dictionary<string, PredefinedField>();
            }
            return documentResult;
        }

        private static void ReadPropertyValue(ref PredefinedFieldExtractionInternal documentResult, JsonProperty property)
        {
            if (property.NameEquals("docType"))
            {
                documentResult.DocumentType = property.Value.GetString();
            }
            else if (property.NameEquals("pageRange"))
            {
                var array = property.Value.EnumerateArray();
                var start = array.Current.GetInt32();
                array.MoveNext();
                var end = array.Current.GetInt32();
                documentResult.FirstPageNumber = start;
                documentResult.LastPageNumber = end;
            }
            else if (property.NameEquals("fields"))
            {
                documentResult.Fields = ObjectJson.Read(property.Value, PredefinedFieldJson.Read);
            }
        }
    }
}