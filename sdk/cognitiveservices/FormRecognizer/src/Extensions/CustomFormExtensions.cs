// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
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

        public static Request CreateListModelsRequest(this HttpPipeline pipeline, string nextLink = default, string op = default)
        {
            var request = pipeline.CreateRequest();
            request.Method = RequestMethod.Get;
            if (!string.IsNullOrEmpty(op))
            {
                request.Uri.AppendQuery("op", op);
            }
            if (string.IsNullOrEmpty(nextLink))
            {
                request.Uri.Path = "/custom/models";
            }
            else
            {
                request.Uri.Reset(new Uri(nextLink, UriKind.Absolute));
            }
            return request;
        }


        public static bool IsModelComplete(this FormModel model)
        {
            return model.ModelInfo.Status != ModelStatus.Creating;
        }

        public static bool IsModelSuccess(this FormModel model)
        {
            return model.ModelInfo.Status == ModelStatus.Ready;
        }
    }
}