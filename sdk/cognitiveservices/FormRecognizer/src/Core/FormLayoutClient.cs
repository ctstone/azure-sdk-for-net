// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Class to analyze form layout.
    /// </summary>
    public class FormLayoutClient
    {
        private readonly HttpPipeline _pipeline;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class.
        /// </summary>
        internal FormLayoutClient(HttpPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class.
        /// </summary>
        protected FormLayoutClient()
        { }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<Analysis>> AnalyzeAsync(Stream stream, FormContentType contentType, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<Analysis> Analyze(Stream stream, FormContentType contentType, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<Analysis>> AnalyzeAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<Analysis> Analyze(Uri uri, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get operation result.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<Analysis>> AnalyzeAsync(string operationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get operation result.
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<Analysis> Analyze(string operationId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}