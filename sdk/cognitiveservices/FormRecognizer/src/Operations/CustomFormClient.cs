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

namespace Azure.AI.FormRecognizer.Operations
{
    /// <summary>
    /// Manage custom Form Recognizer models.
    /// </summary>
    public class CustomFormClient
    {
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormClient"/> class.
        /// </summary>
        protected CustomFormClient()
        { }

        internal CustomFormClient(HttpPipeline pipeline, FormRecognizerClientOptions options)
        {
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Train.
        /// </summary>
        /// <param name="trainRequest"></param>
        /// <param name="cancellationToken"></param>
        public async virtual Task<TrainingOperation> StartTrainAsync(TrainingRequest trainRequest, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateTrainRequest(trainRequest, _options))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.Created, _options);
                var id = TrainingOperation.GetTrainingOperationId(response);
                return new TrainingOperation(_pipeline, id, _options);
            }
        }

        /// <summary>
        /// Start train.
        /// </summary>
        /// <param name="trainRequest"></param>
        /// <param name="cancellationToken"></param>
        public virtual TrainingOperation StartTrain(TrainingRequest trainRequest, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateTrainRequest(trainRequest, _options))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.Created, _options);
                var id = TrainingOperation.GetTrainingOperationId(response);
                return new TrainingOperation(_pipeline, id, _options);
            }
        }

        /// <summary>
        /// Get status of an existing operation.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual TrainingOperation StartTrain(string operationId, CancellationToken cancellationToken = default)
        {
            return new TrainingOperation(_pipeline, operationId, _options);
        }

        /// <summary>
        /// List Models.
        /// </summary>
        public virtual AsyncPageable<ModelInfo> ListModelsAsync(CancellationToken cancellationToken = default)
        {
            return new ModelsAsyncPageable(_pipeline, _options, cancellationToken);
        }

        /// <summary>
        /// List Models.
        /// </summary>
        public virtual Pageable<ModelInfo> ListModels(CancellationToken cancellationToken = default)
        {
            return new ModelsPageable(_pipeline, _options, cancellationToken);
        }

        /// <summary>
        /// Get models summary.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public async virtual Task<Response<ModelsSummary>> GetModelsSummaryAsync(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateListModelsRequest(op: "summary"))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var listing = await response.GetJsonContentAsync<ModelListing>(_options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(listing.Summary, response);
            }
        }

        /// <summary>
        /// Get models summary.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public virtual Response<ModelsSummary> GetModelsSummary(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateListModelsRequest(op: "summary"))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var summary = response.GetJsonContent<ModelsSummary>(_options);
                return Response.FromValue(summary, response);
            }
        }

        /// <summary>
        /// Get model
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public async virtual Task<Response<FormModel>> GetModelAsync(string modelId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(modelId))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var model = await response.GetJsonContentAsync<FormModel>(_options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(model, response);
            }
        }

        /// <summary>
        /// Get model.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Response<FormModel> GetModel(string modelId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(modelId))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var model = response.GetJsonContent<FormModel>(_options);
                return Response.FromValue(model, response);
            }
        }

        /// <summary>
        /// Get model
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public async virtual Task<Response> DeleteModelAsync(string modelId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateDeleteModelRequest(modelId))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.NoContent, _options);
                return response;
            }
        }

        /// <summary>
        /// Get model.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Response DeleteModel(string modelId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateDeleteModelRequest(modelId))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.NoContent, _options);
                return response;
            }
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public async virtual Task<AnalysisOperation> StartAnalyzeAsync(string modelId, Stream stream, FormContentType? contentType = default, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(modelId, stream, contentType, includeTextDetails))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
            }
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual AnalysisOperation StartAnalyze(string modelId, Stream stream, FormContentType? contentType = default, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(modelId, stream, contentType, includeTextDetails))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
            }
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public async virtual Task<AnalysisOperation> StartAnalyzeAsync(string modelId, Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(modelId, uri, includeTextDetails, _options))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
            }
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual AnalysisOperation StartAnalyze(string modelId, Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(modelId, uri, includeTextDetails, _options))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
            }
        }

        /// <summary>
        /// Get operation result.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual AnalysisOperation StartAnalyze(string modelId, string operationId, CancellationToken cancellationToken = default)
        {
            return new AnalysisOperation(_pipeline, modelId, operationId, _options);
        }

        private AnalysisOperation GetAnalysisOperation(string modelId, Response response)
        {
            response.ExpectStatus(HttpStatusCode.Accepted, _options);
            var id = AnalysisOperation.GetAnalysisOperationId(response);
            return new AnalysisOperation(_pipeline, modelId, id, _options);
        }
    }
}