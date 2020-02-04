// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Arguments;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// The custom form model provides syncronous and asynchronous methods to manage a custom form model. The client
    /// supports retrieving and deleting models. The client also supports analyzing new forms from both
    /// <see cref="Stream" /> and <see cref="Uri" /> objects.
    /// </summary>
    public class CustomFormModelReference : AnalyzeClient
    {
        private readonly string _modelId;

        /// <summary>
        /// Get the current model identifier.
        /// </summary>
        public string ModelId => _modelId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormModelReference"/> class for mocking.
        /// </summary>
        protected CustomFormModelReference()
        { }

        internal CustomFormModelReference(string modelId, HttpPipeline pipeline, JsonSerializerOptions options)
            : base(pipeline, options, GetModelPath(modelId))
        {
            Throw.IfNullOrEmpty(modelId, nameof(modelId));
            Throw.IfMissing(pipeline, nameof(pipeline));
            Throw.IfMissing(options, nameof(options));
            _modelId = modelId;
        }

        /// <summary>
        /// Asynchronously get detailed information about a custom model.
        /// </summary>
        /// <param name="includeKeys">Include list of extracted keys in model information.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<Response<CustomFormModel>> GetAsync(bool? includeKeys = default, CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateGetModelRequest(_modelId, includeKeys))
            using (var response = await Pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, Options);
                var model = await response.GetJsonContentAsync<CustomFormModel>(Options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(model, response);
            }
        }

        /// <summary>
        /// Get detailed information about a custom model.
        /// </summary>
        /// /// <param name="includeKeys">Include list of extracted keys in model information.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response<CustomFormModel> Get(bool? includeKeys = default, CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateGetModelRequest(_modelId, includeKeys))
            using (var response = Pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, Options);
                var model = response.GetJsonContent<CustomFormModel>(Options);
                return Response.FromValue(model, response);
            }
        }

        /// <summary>
        /// Asynchronously mark model for deletion. Model artifacts will be permanently removed within a predetermined period.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<Response> DeleteAsync(CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateDeleteModelRequest(_modelId))
            using (var response = await Pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.NoContent, Options);
                return response;
            }
        }

        /// <summary>
        /// Mark model for deletion. Model artifacts will be permanently removed within a predetermined period.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response Delete(CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateDeleteModelRequest(_modelId))
            using (var response = Pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.NoContent, Options);
                return response;
            }
        }

        internal static string GetModelPath(string modelId)
        {
            Throw.IfNullOrEmpty(modelId, nameof(modelId));
            return $"{CustomFormClient.BasePath}/{modelId}";
        }
    }
}