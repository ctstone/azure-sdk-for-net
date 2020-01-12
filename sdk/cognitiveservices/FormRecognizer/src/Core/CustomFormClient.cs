// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// The CustomForm client provides syncronous and asynchronous methods to manage custom form models. The client
    /// supports training, retrieving, listing, and deleting models. The client also supports analyzing new forms
    /// from either a <see cref="Stream" /> or <see cref="Uri" /> object.
    /// </summary>
    public class CustomFormClient
    {
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomFormClient"/> class for mocking.
        /// </summary>
        protected CustomFormClient()
        { }

        internal CustomFormClient(HttpPipeline pipeline, FormRecognizerClientOptions options)
        {
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Asynchronously create and train a custom model.
        ///
        /// This method returns a <see cref="TrainingOperation" /> that can be used to track the status of the training
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// var op = await client.StartTrainAsync(new TrainingRequest { Source = "https://example.org/" });
        /// var requestId = op.Id;
        /// var model = await op.WaitForCompletionAsync();
        /// ```
        /// </summary>
        /// <param name="trainRequest">
        /// The request must include a `Source` parameter that is either an externally accessible Azure storage
        /// blob container Uri (preferably using a Shared Access Signature) or a valid path to a data folder in a locally
        /// mounted drive (local folders are only supported when accessing an endpoint that is a self-hosted container).
        ///
        /// All training data must be under the source folder or subfolders under it. Models are trained using documents
        /// matching any of the following file extensions:
        ///
        /// - `.pdf`
        /// - `.jpg` / `.jpeg`
        /// - `.png`
        /// - `.tiff` / `.tif`
        ///
        /// Any other files are ignored.
        /// </param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Create and train a custom model.
        ///
        /// This method returns a <see cref="TrainingOperation" /> that can be used to track the status of the training
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// // Wait for completion is only available as an `async` method.
        /// var op = client.TrainAsync(new TrainingRequest { Source = "https://example.org/" });
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
        /// <param name="trainRequest">
        /// The request must include a `Source` parameter that is either an externally accessible Azure storage
        /// blob container Uri (preferably using a Shared Access Signature) or a valid path to a data folder in a locally
        /// mounted drive (local folders are only supported when accessing an endpoint that is a self-hosted container).
        ///
        /// All training data must be under the source folder or subfolders under it. Models are trained using documents
        /// matching any of the following file extensions:
        ///
        /// - `.pdf`
        /// - `.jpg` / `.jpeg`
        /// - `.png`
        /// - `.tiff` / `.tif`
        ///
        /// Any other files are ignored.
        /// </param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Get a <see cref="TrainingOperation" /> status reference to an existhing training request.
        /// </summary>
        /// <param name="operationId">The operation id from a previous training request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual TrainingOperation StartTrain(string operationId, CancellationToken cancellationToken = default)
        {
            return new TrainingOperation(_pipeline, operationId, _options);
        }

        /// <summary>
        /// Asynchronously get information about all custom models.
        ///
        /// This method returns a <see cref="ModelsAsyncPageable" /> that can be used to asynchronously enumerate
        /// all models or list them page-by-page.
        ///
        /// ```csharp
        /// // Enumerate all models (may make multiple service calls):
        /// await foreach (var modelInfo in client.Custom.ListModelsAsync())
        /// {
        ///     Console.WriteLine(modelInfo.ModelId);
        /// }
        ///
        /// // Enumerate pages (may make multiple service calls):
        /// var pages = client.Custom.ListModelsAsync().AsPages();
        ///
        /// // Get individual pages (one service call per operation)
        /// var page1 = client.Custom.ListModelsAsync().GetPageAsync();
        /// var page2 = client.Custom.ListModelsAsync().GetPageAsync(page1.ContinuationToken);
        /// ```
        /// </summary>
        public virtual ModelsAsyncPageable ListModelsAsync(CancellationToken cancellationToken = default)
        {
            return new ModelsAsyncPageable(_pipeline, _options, cancellationToken);
        }

        /// <summary>
        /// Get information about all custom models.
        ///
        /// This method returns a <see cref="ModelsPageable" /> that can be used to snchronously enumerate
        /// all models or list them page-by-page.
        ///
        /// ```csharp
        /// // Enumerate all models (may make multiple service calls):
        /// foreach (var modelInfo in client.Custom.ListModels())
        /// {
        ///     Console.WriteLine(modelInfo.ModelId);
        /// }
        ///
        /// // Enumerate pages (may make multiple service calls):
        /// var pages = client.Custom.ListModels().AsPages();
        ///
        /// // Get individual pages (one service call per operation)
        /// var page1 = client.Custom.ListModels().GetPage();
        /// var page2 = client.Custom.ListModels().GetPage(page1.ContinuationToken);
        /// ```
        /// </summary>
        public virtual ModelsPageable ListModels(CancellationToken cancellationToken = default)
        {
            return new ModelsPageable(_pipeline, _options, cancellationToken);
        }

        /// <summary>
        /// Asynchronously get summary of all models.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Get summary of all models.
        /// </summary>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Asynchronously get detailed information about a custom model.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Get detailed information about a custom model.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Asynchronously mark model for deletion. Model artifacts will be permanently removed within a predetermined period.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// Mark model for deletion. Model artifacts will be permanently removed within a predetermined period.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
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
        /// <param name="modelId">Model identifier.</param>
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
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<AnalysisOperation> StartAnalyzeAsync(string modelId, Stream stream, FormContentType? contentType = default, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(modelId, stream, contentType, includeTextDetails))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
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
        /// <param name="modelId">Model identifier.</param>
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
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalysisOperation StartAnalyze(string modelId, Stream stream, FormContentType? contentType = default, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeStreamRequest(modelId, stream, contentType, includeTextDetails))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
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
        /// <param name="modelId">Model identifier.</param>
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
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<AnalysisOperation> StartAnalyzeAsync(string modelId, Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(modelId, uri, includeTextDetails, _options))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
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
        /// <param name="modelId">Model identifier.</param>
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
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalysisOperation StartAnalyze(string modelId, Uri uri, bool? includeTextDetails = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateAnalyzeUriRequest(modelId, uri, includeTextDetails, _options))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                return GetAnalysisOperation(modelId, response);
            }
        }

        /// <summary>
        /// Get an <see cref="AnalysisOperation" /> status reference to an existhing analysis request.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual AnalysisOperation StartAnalyze(string modelId, string operationId, CancellationToken cancellationToken = default)
        {
            return new AnalysisOperation(_pipeline, modelId, operationId, _options);
        }

        /// <summary>
        /// Asynchronously get the analysis result of an analysis operation.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async virtual Task<Response<Analysis>> GetAnalysisResultAsync(string modelId, string operationId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(modelId, operationId))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var analysis = await response.GetJsonContentAsync<Analysis>(_options, cancellationToken).ConfigureAwait(false);
                return Response.FromValue(analysis, response);
            }
        }

        /// <summary>
        /// Get the analysis result of an analysis operation.
        /// </summary>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="operationId">The operation id from a previous analysis request.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public virtual Response<Analysis> GetAnalysisResult(string modelId, string operationId, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(modelId, operationId))
            using (var response = _pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var analysis = response.GetJsonContent<Analysis>(_options);
                return Response.FromValue(analysis, response);
            }
        }

        private AnalysisOperation GetAnalysisOperation(string modelId, Response response)
        {
            response.ExpectStatus(HttpStatusCode.Accepted, _options);
            var id = AnalysisOperation.GetAnalysisOperationId(response);
            return new AnalysisOperation(_pipeline, modelId, id, _options);
        }
    }
}