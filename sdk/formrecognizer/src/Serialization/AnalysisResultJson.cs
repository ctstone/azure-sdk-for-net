// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class AnalysisResultJson
    {
        public static AnalysisResult Read(JsonElement root)
        {
            var analysisResult = AnalysisResult.Create();

            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref analysisResult, property);
                }
            }

            if (analysisResult.ReadResults == default)
            {
                analysisResult.ReadResults = Array.Empty<OcrExtractedPage>();
            }
            if (analysisResult.PageResults == default)
            {
                analysisResult.PageResults = Array.Empty<PageResult>();
            }
            if (analysisResult.DocumentResults == default)
            {
                analysisResult.DocumentResults = Array.Empty<ExtractedForm>();
            }
            if (analysisResult.Errors == default)
            {
                analysisResult.Errors = Array.Empty<FormRecognizerError>();
            }
            //foreach (var page in analysisResult.PageResults)
            //{
            //    foreach (var keyValuePair in page.KeyValuePairs)
            //    {
            //        if (keyValuePair.Key != default)
            //        {
            //            keyValuePair.Key.ResolveTextReferences(analysisResult.ReadResults);
            //        }
            //        if (keyValuePair.Value != default)
            //        {
            //            keyValuePair.Value.ResolveTextReferences(analysisResult.ReadResults);
            //        }
            //    }
            //    foreach (var table in page.Tables)
            //    {
            //        foreach (var cell in table.Cells)
            //        {
            //            cell.ResolveTextReferences(analysisResult.ReadResults);
            //        }
            //    }
            //}
            //foreach (var documentResult in analysisResult.DocumentResults)
            //{
            //    foreach (var field in documentResult.FormFields)
            //    {
            //        if (field.Value != default)
            //        {
            //            field.Value.ResolveTextReferences(analysisResult.ReadResults);
            //        }
            //    }
            //}
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
                analyzedForm.ReadResults = new OcrExtractedPage[property.Value.GetArrayLength()];
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
                analyzedForm.DocumentResults = new ExtractedForm[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.DocumentResults[i] = DocumentResultJson.Read(json);
                    i += 1;
                }
            }
            else if (property.NameEquals("errors"))
            {
                analyzedForm.Errors = new FormRecognizerError[property.Value.GetArrayLength()];
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