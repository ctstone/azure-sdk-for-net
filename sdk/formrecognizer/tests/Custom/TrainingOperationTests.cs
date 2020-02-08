// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Custom;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Tests.TestUtilities;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Custom
{
    public class TrainingOperationTests
    {
        private const string FakeOperationId = "123";

        [Fact]
        public void Operation_Initializes()
        {
            var op = GetOperation();
            Assert.Equal(FakeOperationId, op.Id);
            Assert.False(op.HasCompleted);
            Assert.False(op.HasValue);
            Assert.Null(op.Value);
            Assert.Null(op.GetRawResponse());
        }

        [Theory]
        [InlineData(true, "creating", false, false)]
        [InlineData(false, "creating", false, false)]
        [InlineData(true, "ready", true, true)]
        [InlineData(false, "ready", true, true)]
        [InlineData(true, "invalid", true, false)]
        [InlineData(false, "invalid", true, false)]
        public async Task UpdateStatus_Completes_On200(bool isAsync, string testStatus, bool expectCompleted, bool expectValue)
        {
            // Arrange
            var mockResponse = MockFormResponses.GetModelResponse(testStatus);
            var op = GetOperation(mockResponse);

            // Act
            var response = isAsync
                ? await op.UpdateStatusAsync()
                : op.UpdateStatus();

            // Assert
            Assert.NotNull(response);
            Assert.Equal(response, op.GetRawResponse());
            Assert.Equal(expectCompleted, op.HasCompleted);
            Assert.Equal(expectValue, op.HasValue);
            if (expectValue)
            {
                Assert.NotNull(op.Value);
            }
            else
            {
                Assert.Null(op.Value);
            }
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task UpdateStatus_Throws_On400(bool isAsync)
        {
            // Arrange
            var mockResponse = MockFormResponses.GetErrorResponse(HttpStatusCode.BadRequest, "123", "foo");
            var op = GetOperation(mockResponse);

            // Act / Assert
            var ex = isAsync
                ? await Assert.ThrowsAsync<RequestFailedException>(async () => await op.UpdateStatusAsync())
                : Assert.Throws<RequestFailedException>(() => op.UpdateStatus());
            Assert.Equal("123", ex.ErrorCode);
            Assert.Contains("foo", ex.Message);
        }

        [Theory(Timeout = 2000)]
        [InlineData("ready", ModelStatus.Ready)]
        [InlineData("invalid", ModelStatus.Invalid)]
        public async Task WaitForCompletion_ReturnsAnalysis_On200(string finalStatus, ModelStatus expectStatus)
        {
            var responses = new[]
                {
                    MockFormResponses.GetModelResponse("creating"),
                    MockFormResponses.GetModelResponse("creating"),
                    MockFormResponses.GetModelResponse(finalStatus),
                }
                .ToArray();
            var op = GetOperation(responses);

            // Act
            var response = await op.WaitForCompletionAsync(TimeSpan.FromSeconds(0.1));

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Value);
            Assert.NotNull(response.Value.Information);
            Assert.Equal(expectStatus, response.Value.Information.Status);
        }

        private TrainingOperation<FormModel> GetOperation(params MockResponse[] responses)
        {
            var mockTransport = new MockTransport(responses);
            var pipeline = new HttpPipeline(mockTransport);
            var options = new FormClientOptions();
            return new TrainingOperation<FormModel>(pipeline, FakeOperationId, options.SerializationOptions, (model) => new FormModel(model));
        }
    }
}