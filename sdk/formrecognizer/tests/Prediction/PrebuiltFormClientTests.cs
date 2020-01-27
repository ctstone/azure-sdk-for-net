// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Prediction;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Prediction
{
    public class PrebuiltFormClientTests
    {
        [Fact]
        public void Constructor_Creates_ReceiptClient()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.NotNull(client.Receipt);
        }

        [Fact]
        public void Class_Is_AnalyzeClient()
        {
            // Act
            var client = GetClient();

            // Assert
            Assert.True(client is AnalyzeClient<AnalyzeOptions>);
        }

        private PrebuiltFormClient GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new PrebuiltFormClient(pipeline, options);
        }
    }
}