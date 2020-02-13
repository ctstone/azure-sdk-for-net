// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure.AI.FormRecognizer.Prebuilt;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class ReceiptClientTests
    {
        [Fact]
        public void Class_Is_AnalyzeClient()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.True(client is AnalyzeClient<ReceiptAnalysis>);
        }

        private ReceiptClient GetClient(params MockResponse[] responses)
        {
            return new ReceiptClient(new Uri("http://localhost"), new CognitiveApiKeyCredential("fake-key"));
        }
    }
}