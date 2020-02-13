// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Testing;
using Xunit;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Tests.TestUtilities;

namespace Azure.AI.FormRecognizer.Tests.Extensions
{
    public class ResponseExtensionTests
    {
        [Fact]
        public async Task GetJsonContentAsync_ReturnsObject()
        {
            // Arrange
            var response = MockFormResponses.GetModelResponse("creating");
            var options = new FormClientOptions();

            // Act
            var model = await response.GetJsonContentAsync<CustomFormModelInternal>(options.SerializationOptions, CancellationToken.None);

            // Assert
            Assert.NotNull(model);
            Assert.NotNull(model.ModelInfo);
            Assert.Equal(ModelStatus.Creating, model.ModelInfo.Status);
        }

        [Fact]
        public void GetJsonContent_ReturnsObject()
        {
            // Arrange
            var response = MockFormResponses.GetModelResponse("creating");
            var options = new FormClientOptions();

            // Act
            var model = response.GetJsonContent<CustomFormModelInternal>(options.SerializationOptions);

            // Assert
            Assert.NotNull(model);
            Assert.NotNull(model.ModelInfo);
            Assert.Equal(ModelStatus.Creating, model.ModelInfo.Status);
        }

        [Theory]
        [InlineData(200, 200, true)]
        [InlineData(400, 400, true)]
        [InlineData(500, 500, true)]
        [InlineData(200, 404, false)]
        [InlineData(200, 201, false)]
        [InlineData(404, 200, false)]
        [InlineData(200, 500, false)]
        public void ExpectStatus_Throws_OnUnexpectedStatus(int responseStatus, int expectStatus, bool isValid)
        {
            // Arrange
            var response = new MockResponse(responseStatus);
            var options = new FormClientOptions();

            // Act/Assert
            if (isValid)
            {
                response.ExpectStatus((HttpStatusCode)expectStatus, options.SerializationOptions);
            }
            else
            {
                Assert.Throws<RequestFailedException>(() => response.ExpectStatus((HttpStatusCode)expectStatus, options.SerializationOptions));
            }
        }
    }
}