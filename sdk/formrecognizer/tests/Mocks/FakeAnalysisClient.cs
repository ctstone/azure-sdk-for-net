// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Tests.Mocks
{
    internal class FakeAnalysisClient : AnalyzeClient<FakeAnalysisClient.FakeAnalysis>
    {
        public FakeAnalysisClient(HttpPipeline pipeline, JsonSerializerOptions options)
            : base(pipeline, options, "/fake", (analysis) => new FakeAnalysis(analysis))
        {
        }

        internal class FakeAnalysis
        {
            public AnalysisInternal Analysis { get; }
            public FakeAnalysis(AnalysisInternal analysis)
            {
                Analysis = analysis;
            }
        }
    }
}