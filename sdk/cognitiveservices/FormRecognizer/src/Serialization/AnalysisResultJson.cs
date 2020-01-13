// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class AnalysisResultJson
    {
        public static AnalysisResult Read(JsonElement root)
        {
            var analysisResult = AnalysisResult.Create();
            foreach (JsonProperty property in root.EnumerateObject())
            {
                ReadPropertyValue(ref analysisResult, property);
            }
            return analysisResult;
        }

        private static void ReadPropertyValue(ref AnalysisResult analyzedForm, JsonProperty property)
        {
            if (property.NameEquals("version"))
            {
                analyzedForm.Version = property.Value.GetString();
            }
            else if (property.NameEquals("readResults"))
            {
                analyzedForm.ReadResults = new ReadResult[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.ReadResults[i] = ReadResultJson.Read(json);
                    i += 1;
                }
            }
            else if (property.NameEquals("pageResults"))
            {
                analyzedForm.PageResults = new PageResult[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.PageResults[i] = PageResultJson.Read(json);
                    i += 1;
                }
            }
            else if (property.NameEquals("documentResults"))
            {
                analyzedForm.DocumentResults = new DocumentResult[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.DocumentResults[i] = DocumentResultJson.Read(json);
                    i += 1;
                }
            }
            else if (property.NameEquals("errors"))
            {
                analyzedForm.Errors = new ErrorDetails[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.Errors[i] = ErrorDetailsJson.Read(json);
                    i += 1;
                }
            }
        }
    }
}