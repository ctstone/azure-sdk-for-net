// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer
{
    internal static class ModelExtensions
    {
        public static bool IsModelComplete(this FormModel model)
        {
            return model.ModelInfo.Status != ModelStatus.Creating;
        }

        public static bool IsModelSuccess(this FormModel model)
        {
            return model.ModelInfo.Status == ModelStatus.Ready;
        }

        public static bool IsAnalysisComplete(this Analysis analysis)
        {
            return analysis.Status == AnalysisStatus.Succeeded || analysis.Status == AnalysisStatus.Failed;
        }

        public static bool IsAnalysisSuccess(this Analysis analysis)
        {
            return analysis.Status == AnalysisStatus.Succeeded;
        }
    }
}