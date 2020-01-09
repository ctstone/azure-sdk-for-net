// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class CustomFormExtensions
    {
        public static Request CreateTrainRequest(this HttpPipeline pipeline, TrainRequest trainRequest, FormRecognizerClientOptions options)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Post;
            request.Uri.Path = "/custom/models";
            request.AddJsonContent(trainRequest, options);
            return request;
        }

        public static Request CreateGetModelRequest(this HttpPipeline pipeline, string modelId)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            request.Uri.Path = $"/custom/models/{modelId}";
            return request;
        }

        public static bool IsModelComplete(this CustomFormModel model)
        {
            return model.ModelInfo.Status != ModelStatus.Creating;
        }

        public static bool IsModelSuccess(this CustomFormModel model)
        {
            return model.ModelInfo.Status == ModelStatus.Ready;
        }
    }
}