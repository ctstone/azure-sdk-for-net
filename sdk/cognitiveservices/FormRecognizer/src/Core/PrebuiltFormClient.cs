// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Extensions.Prebuilt;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Class to analyze form layout.
    /// </summary>
    public abstract class PrebuiltFormClient
    {
        internal const string BasePath = "/prebuilt";
        private readonly HttpPipeline _pipeline;
        private readonly string _prebuiltName;
        private readonly FormRecognizerClientOptions _options;

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

        internal PrebuiltFormClient(string prebuiltName, HttpPipeline pipeline, FormRecognizerClientOptions options)
        {
            _pipeline = pipeline;
            _prebuiltName = prebuiltName;
            _options = options;
        }

        /// <summary>
        /// Asynchronously extract field text and semantic values from a given receipt document. The input document must
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
        /// If the `contentType` parameter is not provided, and the stream is seekable, the Form Recognizer client will attempt
        /// to infer the content type from its binary content.
        /// </param>
        /// <param name="contentType">
        /// The content type of the provided `stream`. Leave as `null` to let the Form Recognizer client infer the type
        /// from the seekable stream's binary content.
        ///
        /// If this parameter is null, the provided `stream` must be seekable.
        /// </param>
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<AnalysisOperation> StartAnalyzeAsync(Stream stream, FormContentType contentType, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(_prebuiltName, stream, contentType, includeTextDetails))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return AnalysisOperation.FromResponse(_pipeline, GetModelPath(_prebuiltName), response, _options);
            }
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="contentType"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<Analysis> Analyze(Stream stream, FormContentType contentType, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Task<Operation<Analysis>> AnalyzeAsync(Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Analyze form.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="includeTextDetails"></param>
        /// <param name="cancellationToken"></param>
        public virtual Operation<Analysis> Analyze(Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
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

        internal static string GetModelPath(string modelName)
        {
            Throw.IfMissing(modelName, nameof(modelName));
            return $"{BasePath}/{modelName}";
        }
    }
}