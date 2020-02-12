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
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// Defines the synchronous and asynchronous operations to analyze forms and retrieve results.
    /// Supports analyzing files from both <see cref="Stream" /> and <see cref="Uri" /> objects.
    /// </summary>
    public abstract class AnalyzeClient<TAnalysis>
        where TAnalysis : class
    {
        private readonly HttpPipeline _pipeline;
        private readonly string _basePath;
        private readonly JsonSerializerOptions _options;
        private Func<AnalysisInternal, TAnalysis> _analysisFactory;

        /// <summary>
        /// Get the HTTP pipeline.
        /// </summary>
        protected HttpPipeline Pipeline => _pipeline;

        /// <summary>
        /// Get the base path.
        /// </summary>
        protected string BasePath => _basePath;

        /// <summary>
        /// Get the JSON serializer options.
        /// </summary>
        protected JsonSerializerOptions Options => _options;

        /// <summary>
        /// /// Initializes a new instance of the <see cref="AnalyzeClient{TAnalysis>"/> class.
        /// </summary>
        protected AnalyzeClient()
        { }

        internal AnalyzeClient(HttpPipeline pipeline, JsonSerializerOptions options, string basePath, Func<AnalysisInternal, TAnalysis> analysisFactory)
        {
            _pipeline = pipeline;
            _basePath = basePath;
            _options = options;
            _analysisFactory = analysisFactory;
        }

        /// <summary>
        /// Get the analysis result of an analysis operation.
        /// </summary>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response<TAnalysis> GetAnalysisResult(string operationId, CancellationToken cancellationToken = default)
        {
            Throw.IfNullOrEmpty(operationId, nameof(operationId));
            using (var request = _pipeline.CreateGetAnalysisRequest(_basePath, operationId))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var analysis = response.GetJsonContent<AnalysisInternal>(_options);
                return Response.FromValue(_analysisFactory(analysis), response);
            }
        }

        /// <summary>
        /// Asynchronously get the analysis result of an analysis operation.
        /// </summary>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual async Task<Response<TAnalysis>> GetAnalysisResultAsync(string operationId, CancellationToken cancellationToken = default)
        {
            Throw.IfNullOrEmpty(operationId, nameof(operationId));
            using (var request = _pipeline.CreateGetAnalysisRequest(_basePath, operationId))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var analysis = await response.GetJsonContentAsync<AnalysisInternal>(_options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(_analysisFactory(analysis), response);
            }
        }

        /// <summary>
        /// Extract key-value pairs, tables, and semantic values from a given document. The input document must
        /// be of one of the supported content types:
        ///
        /// - `application/pdf`
        /// - `image/jpeg`
        /// - `image/png`
        /// - `image/tiff`
        ///
        /// This method returns an <see cref="AnalyzeOperation{TAnalysis}" /> that can be used to track the status of the training
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// var file = System.IO.File.OpenRead("/path/to/my/file");
        /// var op = await client.StartAnalyzeFile("modelId", file);
        /// var requestId = op.Id;
        /// while (!op.HasCompleted)
        /// {
        ///     op.UpdateStatus()
        ///     Thread.Sleep(1000);
        /// }
        /// if (op.HasValue)
        /// {
        ///     var model = op.Value
        /// }
        /// ```
        /// </summary>
        /// <param name="stream">
        /// Any stream representing a local file to be analyzed. The file must be one of:
        ///
        /// - `.pdf`
        /// - `.png`
        /// - `.jpeg` / `.jpg`
        /// - `.tiff` / `.tif`
        ///
        /// If the `contentType` parameter is not provided, and the stream is seekable, the Form Recognizer will attempt
        /// to infer the content type from its binary content.
        /// </param>
        /// <param name="contentType">
        /// The content type of the provided `stream`. Leave as `null` to let the Form Recognizer client infer the type
        /// from the seekable stream's binary content.
        ///
        /// If this parameter is null, the provided `stream` must be seekable.
        /// </param>
        /// <param name="includeTextDetails">Optional parameter to include extra details in the response.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalyzeOperation<TAnalysis> StartAnalyze(Stream stream, FormContentType? contentType = null, bool? includeTextDetails = default, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(_basePath, stream, contentType, includeTextDetails))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(response);
            }
        }

        /// <summary>
        /// Extract key-value pairs, tables, and semantic values from a given document. The input document must
        /// be of one of the supported content types:
        ///
        /// - `application/pdf`
        /// - `image/jpeg`
        /// - `image/png`
        /// - `image/tiff`
        ///
        /// This method returns an <see cref="AnalyzeOperation{TAnalysis}" /> that can be used to track the status of the training
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// var file = System.IO.File.OpenRead("/path/to/my/file");
        /// var op = await client.StartAnalyzeFileAsync("modelId", file);
        /// var requestId = op.Id;
        /// var results = await op.WaitForCompletionAsync();
        /// ```
        /// </summary>
        /// <param name="uri">
        /// The absolute URI of the remote document that will be analyzed.
        ///
        /// The resource at this location must respond with a `Content-Type` header that matches one of the supported document
        /// types:
        ///
        /// - `application/pdf`
        /// - `image/jpeg`
        /// - `image/png`
        /// - `image/tiff`
        /// </param>
        /// <param name="includeTextDetails">Optional parameter to include extra details in the response.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalyzeOperation<TAnalysis> StartAnalyze(Uri uri, bool? includeTextDetails = default, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(_basePath, uri, includeTextDetails, _options))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(response);
            }
        }

        /// <summary>
        /// Get an <see cref="AnalyzeOperation{TAnalysis}" /> status reference to an existhing analysis request.
        /// </summary>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalyzeOperation<TAnalysis> StartAnalyze(string operationId, CancellationToken cancellationToken = default)
        {
            return new AnalyzeOperation<TAnalysis>(_pipeline, _basePath, operationId, _options, _analysisFactory);
        }

        /// <summary>
        /// Asynchronously extract key-value pairs, tables, and semantic values from a given document. The input document must
        /// be of one of the supported content types:
        ///
        /// - `application/pdf`
        /// - `image/jpeg`
        /// - `image/png`
        /// - `image/tiff`
        ///
        /// This method returns an <see cref="AnalyzeOperation{TAnalysis}" /> that can be used to track the status of the analysis
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// var file = System.IO.File.OpenRead("/path/to/my/file");
        /// var op = await client.StartAnalyzeFileAsync("modelId", file);
        /// var requestId = op.Id;
        /// var results = await op.WaitForCompletionAsync();
        /// ```
        /// </summary>
        /// <param name="stream">
        /// Any stream representing a local file to be analyzed. The file must be one of:
        ///
        /// - `.pdf`
        /// - `.png`
        /// - `.jpeg` / `.jpg`
        /// - `.tiff` / `.tif`
        ///
        /// If the `contentType` parameter is not provided, and the stream is seekable, the Form Recognizer will attempt
        /// to infer the content type from its binary content.
        /// </param>
        /// <param name="contentType">
        /// The content type of the provided `stream`. Leave as `null` to let the Form Recognizer client infer the type
        /// from the seekable stream's binary content.
        ///
        /// If this parameter is null, the provided `stream` must be seekable.
        /// </param>
        /// <param name="includeTextDetails">Optional parameter to include extra details in the response.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual async Task<AnalyzeOperation<TAnalysis>> StartAnalyzeAsync(Stream stream, FormContentType? contentType = default, bool? includeTextDetails = default, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(_basePath, stream, contentType, includeTextDetails))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(response);
            }
        }

        /// <summary>
        /// Asynchronously extract key-value pairs, tables, and semantic values from a given document. The input document must
        /// be of one of the supported content types:
        ///
        /// - `application/pdf`
        /// - `image/jpeg`
        /// - `image/png`
        /// - `image/tiff`
        ///
        /// This method returns an <see cref="AnalyzeOperation{TAnalysis}" /> that can be used to track the status of the training
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// var file = System.IO.File.OpenRead("/path/to/my/file");
        /// var op = await client.StartAnalyzeFileAsync("modelId", file);
        /// var requestId = op.Id;
        /// var results = await op.WaitForCompletionAsync();
        /// ```
        /// </summary>
        /// <param name="uri">
        /// The absolute URI of the remote document that will be analyzed.
        ///
        /// The resource at this location must respond with a `Content-Type` header that matches one of the supported document
        /// types:
        ///
        /// - `application/pdf`
        /// - `image/jpeg`
        /// - `image/png`
        /// - `image/tiff`
        /// </param>
        /// <param name="includeTextDetails">Optional parameter to include extra details in the response.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual async Task<AnalyzeOperation<TAnalysis>> StartAnalyzeAsync(Uri uri, bool? includeTextDetails = default, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(_basePath, uri, includeTextDetails, _options))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(response);
            }
        }

        private AnalyzeOperation<TAnalysis> GetAnalysisOperation(Response response)
        {
            response.ExpectStatus(HttpStatusCode.Accepted, _options);
            var id = AnalyzeOperation<TAnalysis>.GetAnalysisOperationId(response);
            return new AnalyzeOperation<TAnalysis>(_pipeline, _basePath, id, _options, _analysisFactory);
        }
    }
}