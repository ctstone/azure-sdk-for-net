// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Layout;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class LayoutClientTests
    {
        [Fact]
        public void Class_Is_AnalyzeClient()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.True(client is AnalyzeClient<LayoutAnalysis>);
        }

        private LayoutClient GetClient(params MockResponse[] responses)
        {
            return new LayoutClient(new Uri("http://localhost"), new CognitiveApiKeyCredential("fake-key"));
        }
    }
}