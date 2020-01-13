// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// The CustomFormModelClient provides syncronous and asynchronous methods to manage a custom form model. The client
    /// supports retrieving and deleting models. The client also supports analyzing new forms from either both
    /// <see cref="Stream" /> and <see cref="Uri" /> objects.
    /// </summary>
    public class CustomFormModelClient : AnalysisClient
    {
        private readonly string _modelId;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormClient"/> class for mocking.
        /// </summary>
        protected CustomFormModelClient()
        { }

        internal CustomFormModelClient(string modelId, HttpPipeline pipeline, FormRecognizerClientOptions options)
            : base(pipeline, options, GetModelPath(modelId))
        {
            _modelId = modelId;
        }

        /// <summary>
        /// Asynchronously get detailed information about a custom model.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<Response<Model>> GetAsync(CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateGetModelRequest(_modelId))
            using (var response = await Pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, Options);
                var model = await response.GetJsonContentAsync<Model>(Options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(model, response);
            }
        }

        /// <summary>
        /// Get detailed information about a custom model.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response<Model> Get(CancellationToken cancellationToken = default)
        {
            using (var request = Pipeline.CreateGetModelRequest(_modelId))
            using (var response = Pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, Options);
                var model = response.GetJsonContent<Model>(Options);
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
            Throw.IfMissing(modelId, nameof(modelId));
            return $"{CustomFormClient.BasePath}/{modelId}";
        }
    }
}