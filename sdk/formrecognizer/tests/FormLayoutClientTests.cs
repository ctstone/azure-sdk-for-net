// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Prediction;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class FormLayoutClientTests
    {
        [Fact]
        public void Class_Is_AnalyzeClient()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.True(client is AnalyzeClient<AnalyzeLayoutOptions>);
        }

        private FormLayoutClient GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new FormLayoutClient(pipeline, options);
        }
    }
}