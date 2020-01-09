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
    /// Class to analyze form layout.
    /// </summary>
    public abstract class PrebuiltFormClient
    {
        private readonly HttpPipeline _pipeline;
        private readonly string _prebuiltName;

        /// <summary>
        /// Prebuilt name.
        /// </summary>
        protected string PrebuiltName => _prebuiltName;

        /// <summary>
        /// Pipeline.
        /// </summary>
        protected HttpPipeline Pipeline => _pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrebuiltFormClient"/> class.
        /// </summary>
        protected PrebuiltFormClient()
        { }

        internal PrebuiltFormClient(string prebuiltName, HttpPipeline pipeline)
        {
            _pipeline = pipeline;
            _prebuiltName = prebuiltName;
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<AnalyzedForm>> AnalyzeAsync(Stream stream, FormContentType contentType, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<AnalyzedForm> Analyze(Stream stream, FormContentType contentType, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<AnalyzedForm>> AnalyzeAsync(Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<AnalyzedForm> Analyze(Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
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