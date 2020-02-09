// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;
using Xunit;

namespace Azure.AI.FormRecognizer.Tests.Extensions
{
    public class ModelExtensionTests
    {
        [Theory]
        [InlineData(ModelStatus.Creating, false)]
        [InlineData(ModelStatus.Ready, true)]
        [InlineData(ModelStatus.Invalid, true)]
        public void IsModelComplete_ReturnsTrue_WhenComplete(ModelStatus testStatus, bool expectComplete)
        {
            // Arrange
            var model = CustomFormModel.Create();
            model.ModelInfo = ModelInfo.Create();
            model.ModelInfo.Status = testStatus;

            // Act
            var complete = ModelExtensions.IsModelComplete(model);

            // Assert
            Assert.Equal(expectComplete, complete);
        }

        [Theory]
        [InlineData(ModelStatus.Creating, false)]
        [InlineData(ModelStatus.Ready, true)]
        [InlineData(ModelStatus.Invalid, false)]
        public void IsModelSuccess_ReturnsTrue_WhenReady(ModelStatus testStatus, bool expectSuccess)
        {
            // Arrange
            var model = CustomFormModel.Create();
            model.ModelInfo = ModelInfo.Create();
            model.ModelInfo.Status = testStatus;

            // Act
            var success = ModelExtensions.IsModelSuccess(model);

            // Assert
            Assert.Equal(expectSuccess, success);
        }

        [Theory]
        [InlineData(OperationStatus.NotStarted, false)]
        [InlineData(OperationStatus.Running, false)]
        [InlineData(OperationStatus.Succeeded, true)]
        [InlineData(OperationStatus.Failed, true)]
        public void IsAnalysisComplete_ReturnsTrue_WhenComplete(OperationStatus testStatus, bool expectComplete)
        {
            // Arrange
            var analysis = AnalysisInternal.Create();
            analysis.Status = testStatus;

            // Act
            var complete = ModelExtensions.IsAnalysisComplete(analysis);

            // Assert
            Assert.Equal(expectComplete, complete);
        }

        [Theory]
        [InlineData(OperationStatus.NotStarted, false)]
        [InlineData(OperationStatus.Running, false)]
        [InlineData(OperationStatus.Succeeded, true)]
        [InlineData(OperationStatus.Failed, false)]
        public void IsAnalysisSuccess_ReturnsTrue_WhenReady(OperationStatus testStatus, bool expectSuccess)
        {
            // Arrange
            var analysis = AnalysisInternal.Create();
            analysis.Status = testStatus;

            // Act
            var success = ModelExtensions.IsAnalysisSuccess(analysis);

            // Assert
            Assert.Equal(expectSuccess, success);
        }
    }
}