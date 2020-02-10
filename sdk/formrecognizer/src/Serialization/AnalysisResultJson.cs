// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class AnalysisResultJson
    {
        public static AnalysisResultInternal Read(JsonElement root)
        {
            var analysisResult = AnalysisResultInternal.Create();

            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref analysisResult, property);
                }
            }

            if (analysisResult.TextExtractionPages == default)
            {
                analysisResult.TextExtractionPages = Array.Empty<TextExtractionPage>();
            }
            if (analysisResult.FieldExtractionPages == default)
            {
                analysisResult.FieldExtractionPages = Array.Empty<FieldExtractionPageInternal>();
            }
            if (analysisResult.PredefinedFieldExtractions == default)
            {
                analysisResult.PredefinedFieldExtractions = Array.Empty<PredefinedFormInternal>();
            }
            if (analysisResult.Errors == default)
            {
                analysisResult.Errors = Array.Empty<ErrorDetails>();
            }
            foreach (var page in analysisResult.FieldExtractionPages)
            {
                foreach (var keyValuePair in page.Fields)
                {
                    if (keyValuePair.Field != default)
                    {
                        keyValuePair.Field.ResolveTextReferences(analysisResult.TextExtractionPages);
                    }
                    if (keyValuePair.Value != default)
                    {
                        keyValuePair.Value.ResolveTextReferences(analysisResult.TextExtractionPages);
                    }
                }
                foreach (var table in page.Tables)
                {
                    foreach (var cell in table.Cells)
                    {
                        cell.ResolveTextReferences(analysisResult.TextExtractionPages);
                    }
                }
            }
            foreach (var documentResult in analysisResult.PredefinedFieldExtractions)
            {
                foreach (var field in documentResult.Fields)
                {
                    if (field.Value != default)
                    {
                        field.Value.ResolveTextReferences(analysisResult.TextExtractionPages);
                    }
                }
            }
            return analysisResult;
        }

        private static void ReadPropertyValue(ref AnalysisResultInternal analyzedForm, JsonProperty property)
        {
            if (property.NameEquals("version"))
            {
                analyzedForm.Version = property.Value.GetString();
            }
            else if (property.NameEquals("readResults"))
            {
                analyzedForm.TextExtractionPages = new TextExtractionPage[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.TextExtractionPages[i] = TextExtractionPageJson.Read(json);
                    i += 1;
                }
            }
            else if (property.NameEquals("pageResults"))
            {
                analyzedForm.FieldExtractionPages = new FieldExtractionPageInternal[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.FieldExtractionPages[i] = FieldExtractionPageJson.Read(json);
                    i += 1;
                }
            }
            else if (property.NameEquals("documentResults"))
            {
                analyzedForm.PredefinedFieldExtractions = new PredefinedFormInternal[property.Value.GetArrayLength()];
                var i = 0;
                foreach (var json in property.Value.EnumerateArray())
                {
                    analyzedForm.PredefinedFieldExtractions[i] = PredefinedFormJson.Read(json);
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