// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer
{
    internal static class ModelExtensions
    {
        public static bool IsModelComplete(this CustomFormModelInternal model)
        {
            return model.ModelInfo.Status != ModelStatus.Creating;
        }

        public static bool IsModelSuccess(this CustomFormModelInternal model)
        {
            return model.ModelInfo.Status == ModelStatus.Ready;
        }

        public static bool IsAnalysisComplete(this AnalysisInternal analysis)
        {
            return analysis.Status == OperationStatus.Succeeded || analysis.Status == OperationStatus.Failed;
        }

        public static bool IsAnalysisSuccess(this AnalysisInternal analysis)
        {
            return analysis.Status == OperationStatus.Succeeded;
        }
    }
}