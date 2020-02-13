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

namespace Azure.AI.FormRecognizer.Custom.Labels
{
    /// <summary>
    /// The custom form model provides syncronous and asynchronous methods to manage a custom form model. The client
    /// supports retrieving and deleting models. The client also supports analyzing new forms from both
    /// <see cref="Stream" /> and <see cref="Uri" /> objects.
    /// </summary>
    public class FormModelWithLabelsReference : AnalyzeClient<FormAnalysisWithLabels>
    {
        private readonly string _modelId;

        /// <summary>
        /// Get the current model identifier.
        /// </summary>
        public string ModelId => _modelId;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormModelWithLabelsReference"/> class for mocking.
        /// </summary>
        protected FormModelWithLabelsReference()
        { }

        internal FormModelWithLabelsReference(string modelId, HttpPipeline pipeline, JsonSerializerOptions options)
            : base(pipeline, options, GetModelPath(modelId), (analysis) => new FormAnalysisWithLabels(analysis))
        {
            Throw.IfNullOrEmpty(modelId, nameof(modelId));
            Throw.IfMissing(pipeline, nameof(pipeline));
            Throw.IfMissing(options, nameof(options));
            _modelId = modelId;
        }

        /// <summary>
        /// Asynchronously get detailed information about a custom model.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<Response<FormModelWithLabels>> GetAsync(CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateGetModelRequest(_modelId, includeKeys: null))
            using (var response = await Pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, Options);
                var model = await response.GetJsonContentAsync<CustomFormModelInternal>(Options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(new FormModelWithLabels(model), response);
            }
        }

        /// <summary>
        /// Get detailed information about a custom model.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response<FormModel> Get(CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateGetModelRequest(_modelId, includeKeys: null))
            using (var response = Pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, Options);
                var model = response.GetJsonContent<CustomFormModelInternal>(Options);
                return Response.FromValue(new FormModel(model), response);
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