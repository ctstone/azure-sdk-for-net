// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
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
        public async virtual Task<Operation<FormModel>> StartTrainAsync(TrainRequest trainRequest, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateTrainRequest(trainRequest, _options))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                var id = TrainOperation.GetTrainingOperationId(response);
                return new TrainOperation(_pipeline, id, _options);
            }
        }

        /// <summary>
        /// Start train.
        /// </summary>
        /// <param name="trainRequest"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<FormModel> StartTrain(TrainRequest trainRequest, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateTrainRequest(trainRequest, _options))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                var id = TrainOperation.GetTrainingOperationId(response);
                return new TrainOperation(_pipeline, id, _options);
            }
        }

        /// <summary>
        /// Get status of an existing operation.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<FormModel> StartTrain(string operationId, CancellationToken cancellationToken = default)
        {
            return new TrainOperation(_pipeline, operationId, _options);
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
                var summary = await response.GetJsonContentAsync<ModelsSummary>(_options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(summary, response);
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
        public virtual Task<Operation<AnalyzedForm>> AnalyzeAsync(string modelId, Stream stream, FormContentType contentType, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<AnalyzedForm> Analyze(string modelId, Stream stream, FormContentType contentType, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<AnalyzedForm>> AnalyzeAsync(string modelId, Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<AnalyzedForm> Analyze(string modelId, Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get operation result.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<AnalyzedForm>> AnalyzeAsync(string operationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get operation result.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<AnalyzedForm> Analyze(string operationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}