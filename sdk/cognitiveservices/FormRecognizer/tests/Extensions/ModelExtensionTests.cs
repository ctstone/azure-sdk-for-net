// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Core;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Core.Testing;
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
            var model = Model.Create();
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
            var model = Model.Create();
            model.ModelInfo = ModelInfo.Create();
            model.ModelInfo.Status = testStatus;

            // Act
            var success = ModelExtensions.IsModelSuccess(model);

            // Assert
            Assert.Equal(expectSuccess, success);
        }

        [Theory]
        [InlineData(AnalysisStatus.NotStarted, false)]
        [InlineData(AnalysisStatus.Running, false)]
        [InlineData(AnalysisStatus.Succeeded, true)]
        [InlineData(AnalysisStatus.Failed, true)]
        public void IsAnalysisComplete_ReturnsTrue_WhenComplete(AnalysisStatus testStatus, bool expectComplete)
        {
            // Arrange
            var analysis = Analysis.Create();
            analysis.Status = testStatus;

            // Act
            var complete = ModelExtensions.IsAnalysisComplete(analysis);

            // Assert
            Assert.Equal(expectComplete, complete);
        }

        [Theory]
        [InlineData(AnalysisStatus.NotStarted, false)]
        [InlineData(AnalysisStatus.Running, false)]
        [InlineData(AnalysisStatus.Succeeded, true)]
        [InlineData(AnalysisStatus.Failed, false)]
        public void IsAnalysisSuccess_ReturnsTrue_WhenReady(AnalysisStatus testStatus, bool expectSuccess)
        {
            // Arrange
            var analysis = Analysis.Create();
            analysis.Status = testStatus;

            // Act
            var success = ModelExtensions.IsAnalysisSuccess(analysis);

            // Assert
            Assert.Equal(expectSuccess, success);
        }
    }
}