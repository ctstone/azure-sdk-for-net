// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Defines the synchronous and asynchronous operations to analyze forms and retrieve results.
    /// Supports analyzing files from both <see cref="Stream" /> and <see cref="Uri" /> objects.
    /// </summary>
    public class AnalysisClient<TOptions>
        where TOptions : struct
    {
        private const string IncludeTextDetailsQueryKey = "includeTextDetails";
        private const string True = "true";
        private const string False = "false";

        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;
        private readonly string _basePath;

        /// <summary>
        /// Get the HTTP pipeline.
        /// </summary>
        protected HttpPipeline Pipeline => _pipeline;

        /// <summary>
        /// Get the Form Recognizer options.
        /// </summary>
        protected FormRecognizerClientOptions Options => _options;

        /// <summary>
        /// Get the base path.
        /// </summary>
        protected string BasePath => _basePath;


        /// <summary>
        /// /// Initializes a new instance of the <see cref="AnalysisClient{TOptions}"/> class.
        /// </summary>
        protected AnalysisClient()
        { }

        internal AnalysisClient(HttpPipeline pipeline, FormRecognizerClientOptions options, string basePath)
        {
            _pipeline = pipeline;
            _options = options;
            _basePath = basePath;
        }

        /// <summary>
        /// Get the analysis result of an analysis operation.
        /// </summary>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response<Analysis> GetAnalysisResult(string operationId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(_basePath, operationId))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var analysis = response.GetJsonContent<Analysis>(_options);
                return Response.FromValue(analysis, response);
            }
        }

        /// <summary>
        /// Asynchronously get the analysis result of an analysis operation.
        /// </summary>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual async Task<Response<Analysis>> GetAnalysisResultAsync(string operationId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(_basePath, operationId))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var analysis = await response.GetJsonContentAsync<Analysis>(_options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(analysis, response);
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
        /// This method returns an <see cref="AnalysisOperation" /> that can be used to track the status of the training
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
        /// <param name="analyzeOptions">Optional analyze options.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalysisOperation StartAnalyze(Stream stream, FormContentType? contentType = null, TOptions? analyzeOptions = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(_basePath, stream, contentType, analyzeOptions, this.ApplyOptions))
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
        /// This method returns an <see cref="AnalysisOperation" /> that can be used to track the status of the training
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
        /// <param name="analyzeOptions">Optional analyze options.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalysisOperation StartAnalyze(Uri uri, TOptions? analyzeOptions = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(_basePath, uri, _options, analyzeOptions, this.ApplyOptions))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(response);
            }
        }

        /// <summary>
        /// Get an <see cref="AnalysisOperation" /> status reference to an existhing analysis request.
        /// </summary>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalysisOperation StartAnalyze(string operationId, CancellationToken cancellationToken = default)
        {
            return new AnalysisOperation(_pipeline, _basePath, operationId, _options);
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
        /// This method returns an <see cref="AnalysisOperation" /> that can be used to track the status of the analysis
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
        /// <param name="analyzeOptions">Optional analyze options.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual async Task<AnalysisOperation> StartAnalyzeAsync(Stream stream, FormContentType? contentType = null, TOptions? analyzeOptions = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(_basePath, stream, contentType, analyzeOptions, this.ApplyOptions))
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
        /// This method returns an <see cref="AnalysisOperation" /> that can be used to track the status of the training
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
        /// <param name="analyzeOptions">Optional analyze options.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual async Task<AnalysisOperation> StartAnalyzeAsync(Uri uri, TOptions? analyzeOptions = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(_basePath, uri, _options, analyzeOptions, this.ApplyOptions))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(response);
            }
        }

        /// <summary>
        /// Apply options to an analyze request.
        /// </summary>
        protected virtual void ApplyOptions(TOptions options, Request request)
        {
        }

        /// <summary>
        /// Apply option to include text details on an analyze request.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="request"></param>
        protected void ApplyAnalyzeOptions(AnalyzeOptions options, Request request)
        {
            request.Uri.AppendQuery(IncludeTextDetailsQueryKey, options.IncludeTextDetails ? True : False);
        }

        private AnalysisOperation GetAnalysisOperation(Response response)
        {
            response.ExpectStatus(HttpStatusCode.Accepted, _options);
            var id = AnalysisOperation.GetAnalysisOperationId(response);
            return new AnalysisOperation(_pipeline, _basePath, id, _options);
        }
    }

    /// <summary>
    /// Options for the analyze operation.
    /// </summary>
    public struct AnalyzeOptions
    {
        /// <summary>
        /// Set to `true` to include text lines and element references in the result.
        /// </summary>
        public bool IncludeTextDetails { get; set; }

        /// <summary>
        /// Create options.
        /// </summary>
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        public AnalyzeOptions(bool includeTextDetails)
        {
            IncludeTextDetails = includeTextDetails;
        }

        /// <summary>
        /// Convert from bool
        /// </summary>
        /// <param name="options">The options.</param>
        public static implicit operator bool(AnalyzeOptions options) => options.IncludeTextDetails;

        /// <summary>
        ///
        /// </summary>
        /// <param name="b"></param>
        public static implicit operator AnalyzeOptions(bool b) => new AnalyzeOptions(b);
    }
}