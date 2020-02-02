// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class FormRecognizerAnalysisClient
    {
        private FormRecognizerClient _formRecognizerClient;

        /// <summary>
        /// </summary>
        protected FormRecognizerAnalysisClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class using a key-based credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        public FormRecognizerAnalysisClient(Uri endpoint, CognitiveKeyCredential credential)
            : this(endpoint, credential, new FormRecognizerAnalysisClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class using a subscription key credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        /// <param name="options">Optional service parameters.</param>
        public FormRecognizerAnalysisClient(Uri endpoint, CognitiveKeyCredential credential, FormRecognizerAnalysisClientOptions options)
        {
            var temp = options.Version;
            _formRecognizerClient = new FormRecognizerClient(endpoint, credential, new FormRecognizerClientOptions());
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual SupervisedAnalysisOperation StartSupervisedAnalysis(string modelId, Stream stream, FormContentType? contentType = null, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = _formRecognizerClient.GetModelReference(modelId).StartAnalyze(stream, contentType, includeTextDetails, cancellationToken);
            return new SupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<SupervisedAnalysisOperation> StartSupervisedAnalysisAsync(string modelId, Stream stream, FormContentType? contentType = null, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = await _formRecognizerClient.GetModelReference(modelId).StartAnalyzeAsync(stream, contentType, includeTextDetails, cancellationToken).ConfigureAwait(false);
            return new SupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual SupervisedAnalysisOperation StartSupervisedAnalysis(string modelId, Uri uri, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = _formRecognizerClient.GetModelReference(modelId).StartAnalyze(uri, includeTextDetails, cancellationToken);
            return new SupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<SupervisedAnalysisOperation> StartSupervisedAnalysisAsync(string modelId, Uri uri, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = await _formRecognizerClient.GetModelReference(modelId).StartAnalyzeAsync(uri, includeTextDetails, cancellationToken).ConfigureAwait(false);
            return new SupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual SupervisedAnalysisOperation StartSupervisedAnalysis(string operationId, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = _formRecognizerClient.GetModelReference("").StartAnalyze(operationId, cancellationToken);
            return new SupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual UnsupervisedAnalysisOperation StartUnsupervisedAnalysis(string modelId, Stream stream, FormContentType? contentType = null, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = _formRecognizerClient.GetModelReference(modelId).StartAnalyze(stream, contentType, includeTextDetails, cancellationToken);
            return new UnsupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<UnsupervisedAnalysisOperation> StartUnsupervisedAnalysisAsync(string modelId, Stream stream, FormContentType? contentType = null, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = await _formRecognizerClient.GetModelReference(modelId).StartAnalyzeAsync(stream, contentType, includeTextDetails, cancellationToken).ConfigureAwait(false);
            return new UnsupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual UnsupervisedAnalysisOperation StartUnsupervisedAnalysis(string modelId, Uri uri, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = _formRecognizerClient.GetModelReference(modelId).StartAnalyze(uri, includeTextDetails, cancellationToken);
            return new UnsupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="modelId"></param>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<UnsupervisedAnalysisOperation> StartUnsupervisedAnalysisAsync(string modelId, Uri uri, bool includeTextDetails = false, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = await _formRecognizerClient.GetModelReference(modelId).StartAnalyzeAsync(uri, includeTextDetails, cancellationToken).ConfigureAwait(false);
            return new UnsupervisedAnalysisOperation(op);
        }

        /// <summary>
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual UnsupervisedAnalysisOperation StartUnsupervisedAnalysis(string operationId, CancellationToken cancellationToken = default)
        {
            AnalyzeOperation op = _formRecognizerClient.GetModelReference("").StartAnalyze(operationId, cancellationToken);
            return new UnsupervisedAnalysisOperation(op);
        }
    }
}
