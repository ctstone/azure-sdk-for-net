// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
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
            Assert.True(client is AnalyzeClient);
        }

        private FormLayoutClient GetClient(params MockResponse[] responses)
        {
            return new FormLayoutClient(new Uri("http://localhost"), new CognitiveKeyCredential("fake-key"));
        }
    }
}