// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Features
{
    /// <summary>
    /// Manage custom Form Recognizer models.
    /// </summary>
    public class CustomFormClient
    {
        private readonly HttpPipeline _pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormClient"/> class.
        /// </summary>
        protected CustomFormClient()
        { }

        internal CustomFormClient(HttpPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        /// <summary>
        /// Train.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<CustomFormModel>> StartTrainAsync(TrainRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get status of operation.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<CustomFormModel>> StartTrainAsync(string operationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Start train.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<CustomFormModel> StartTrain(TrainRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get status of operation.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<CustomFormModel> StartTrain(string operationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// List Models.
        /// </summary>
        public virtual AsyncPageable<ModelInfo> ListModelsAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// List Models.
        /// </summary>
        public virtual Pageable<ModelInfo> ListModels(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get models summary.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public virtual Task<Response<ModelsSummary>> GetModelsSummaryAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get models summary.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public virtual Response<ModelsSummary> GetModelsSummary(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get model
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Response<CustomFormModel>> GetModelAsync(string modelId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get model.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Response<CustomFormModel> GetModel(string modelId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get model
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Response> DeleteModelAsync(string modelId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get model.
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Response DeleteModel(string modelId, CancellationToken cancellationToken = default)
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