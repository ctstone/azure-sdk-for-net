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

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// The CustomForm client provides syncronous and asynchronous methods to manage custom form models. The client
    /// supports training, retrieving, listing, and deleting models. The client also supports analyzing new forms
    /// from either a <see cref="Stream" /> or <see cref="Uri" /> object.
    /// </summary>
    public class CustomFormClient
    {
        internal const string BasePath = "/custom/models";
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
        public async virtual Task<Response<ModelsSummary>> GetSummaryAsync(CancellationToken cancellationToken = default)
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
        public virtual Response<ModelsSummary> GetSummary(CancellationToken cancellationToken = default)
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
        /// Access a model to perform analysis, retrieve metadata or delete it.
        /// </summary>
        /// <param name="modelId">Model identifier</param>
        public virtual CustomFormModelClient UseModel(string modelId) => new CustomFormModelClient(modelId, _pipeline, _options);
    }
}