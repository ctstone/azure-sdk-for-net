// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Custom
{
    public class CustomFormClientTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task StartTrain_ReturnsOperation_On201(bool isAsync)
        {
            // Arrange
            var operationId = "fake-operation-id";
            var mockResponse = new MockResponse((int)HttpStatusCode.Created);
            mockResponse.AddHeader(new HttpHeader("Location", $"http://localhost/{operationId}"));
            var client = GetClient(mockResponse);
            var trainingRequest = new TrainingRequest { Source = "http://localhost/fake-source" };

            // Act
            var operation = isAsync
                ? await client.StartTrainingAsync(trainingRequest)
                : client.StartTraining(trainingRequest);

            // Assert
            Assert.NotNull(operation);
            Assert.Null(operation.Value);
            Assert.NotNull(operation.Id);
            Assert.Equal(operationId, operation.Id);
            Assert.False(operation.HasCompleted);
            Assert.False(operation.HasValue);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task StartTrain_Throws_OnMissingParameter(bool isAsync)
        {
            // Arrange
            var client = GetClient();
            TrainingRequest trainingRequest = null;

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<ArgumentNullException>(() => client.StartTrainingAsync(trainingRequest))
                : Assert.Throws<ArgumentNullException>(() => client.StartTraining(trainingRequest));
            Assert.NotNull(ex.ParamName);
        }

        [Theory]
        [InlineData(true, "")]
        [InlineData(false, "")]
        [InlineData(true, null)]
        [InlineData(false, null)]
        public async Task StartTrain_Throws_OnMissingSource(bool isAsync, string source)
        {
            // Arrange
            var client = GetClient();
            TrainingRequest trainingRequest = new TrainingRequest { Source = source };

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<ArgumentException>(() => client.StartTrainingAsync(trainingRequest))
                : Assert.Throws<ArgumentException>(() => client.StartTraining(trainingRequest));
            Assert.NotNull(ex.ParamName);
        }

        [Fact]
        public void StartTrain_Returns_Operation()
        {
            // Arrange
            var operationId = "fake-operation-id";
            var client = GetClient();

            // Act
            var operation = client.StartTraining(operationId);

            // Assert
            Assert.NotNull(operation);
            Assert.Equal(operationId, operation.Id);
        }

        [Theory]
        [InlineData("", typeof(ArgumentException))]
        [InlineData(null, typeof(ArgumentNullException))]
        public void StartTrain_Throws_OnMissingId(string operationId, Type expectType)
        {
            // Arrange
            var client = GetClient();

            // Act / Assert
            var ex = Assert.Throws(expectType, () => client.StartTraining(operationId));
            Assert.IsAssignableFrom<ArgumentException>(ex);
            ArgumentException argEx = ex as ArgumentException;

            // Assert
            Assert.NotNull(argEx.ParamName);
        }

        [Fact]
        public void ListModelsAsync_Returns_AsyncEnumerable()
        {
            // Arrange
            var client = GetClient();

            // Act
            var listing = client.ListModelsAsync();

            // Assert
            Assert.IsAssignableFrom<IAsyncEnumerable<ModelInfo>>(listing);
        }

        [Fact]
        public void ListModels_Returns_Enumerable()
        {
            // Arrange
            var client = GetClient();

            // Act
            var listing = client.ListModels();

            // Assert
            Assert.IsAssignableFrom<IEnumerable<ModelInfo>>(listing);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetSummary_ReturnsSummary_On200(bool isAsync)
        {
            // Arrange
            var content = @"{ ""summary"": { ""count"": 123, ""limit"": 321, ""lastUpdatedDateTime"": ""2020-01-01T00:00:00Z"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act
            var summary = isAsync
                ? await client.GetSummaryAsync()
                : client.GetSummary();

            // Assert
            Assert.NotNull(summary);
            Assert.NotNull(summary.Value);
            Assert.Equal(123, summary.Value.Count);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetSummary_Throws_On500(bool isAsync)
        {
            // Arrange
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)HttpStatusCode.InternalServerError);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            var client = GetClient(mockResponse);

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<RequestFailedException>(() => client.GetSummaryAsync())
                : Assert.Throws<RequestFailedException>(() => client.GetSummary());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Fact]
        public void UseModel_Returns_ModelClient()
        {
            // Arrange
            var modelId = "fake-model-id";
            var client = GetClient();

            // Act
            var modelRef = client.GetModelReference(modelId);

            // Assert
            Assert.NotNull(modelRef);
            Assert.Equal(modelId, modelRef.ModelId);
        }

        private FormRecognizerClient GetClient(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormRecognizerClientOptions();
            return new FormRecognizerClient(pipeline, options);
        }
    }
}