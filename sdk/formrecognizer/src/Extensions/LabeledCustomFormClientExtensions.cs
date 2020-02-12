// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;

namespace Azure.AI.FormRecognizer.Custom.Labels
{
    /// <summary>
    /// Extensions to support labeled form operations.
    /// </summary>
    public static class LabeledCustomFormClientExtensions
    {
        /// <summary>
        /// Access a model that uses labels to perform analysis, retrieve metadata, or delete it.
        /// </summary>
        /// <param name="client">Custom form client.</param>
        /// <param name="modelId">Model identifier.</param>
        public static LabeledFormModelReference GetModelReferenceWithLabels(this CustomFormClient client, string modelId)
        {
            return new LabeledFormModelReference(modelId, client._pipeline, client._options.SerializationOptions);
        }

        /// <summary>
        /// Access a model that uses labels to perform analysis, retrieve metadata, or delete it.
        /// </summary>
        /// <param name="client">Custom form client.</param>
        /// <param name="modelId">Model identifier.</param>
        public static LabeledFormModelReference<TForm> GetModelReferenceWithLabels<TForm>(this CustomFormClient client, string modelId)
            where TForm : new()
        {
            return new LabeledFormModelReference<TForm>(modelId, client._pipeline, client._options.SerializationOptions);
        }

        /// <summary>
        /// Asynchronously create and train a custom model.
        ///
        /// This method returns a <see cref="TrainingOperation{TModel}" /> that can be used to track the status of the training
        /// operation, including waiting for its completion.
        ///
        /// ```csharp
        /// var op = await client.StartTrainAsync(new TrainingRequest { Source = "https://example.org/" });
        /// var requestId = op.Id;
        /// var model = await op.WaitForCompletionAsync();
        /// ```
        /// </summary>
        /// <param name="client"></param>
        /// <param name="source">
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
        /// <param name="filter">Optional source filter.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public static async Task<TrainingOperation<LabeledFormModel>> StartTrainingWithLabelsAsync(this CustomFormClient client, string source, SourceFilter filter = default, CancellationToken cancellationToken = default)
        {
            using (var request = client._pipeline.CreateTrainRequest(new TrainingRequest(source, filter, useLabelFile: true), client._options.SerializationOptions))
            using (var response = await client._pipeline.SendRequestAsync(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.Created, client._options.SerializationOptions);
                var id = TrainingOperation<LabeledFormModel>.GetTrainingOperationId(response);
                return new TrainingOperation<LabeledFormModel>(client._pipeline, id, client._options.SerializationOptions, client._labeledModelFactory);
            }
        }

        /// <summary>
        /// Create and train a custom model.
        ///
        /// This method returns a <see cref="TrainingOperation{TModel}" /> that can be used to track the status of the training
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
        /// <param name="client"></param>
        /// <param name="source">
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
        /// <param name="filter">Optional source filter.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public static TrainingOperation<LabeledFormModel> StartTrainingWithLabels(this CustomFormClient client, string source, SourceFilter filter = default, CancellationToken cancellationToken = default)
        {
            using (var request = client._pipeline.CreateTrainRequest(new TrainingRequest(source, filter, useLabelFile: true), client._options.SerializationOptions))
            using (var response = client._pipeline.SendRequest(request, cancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.Created, client._options.SerializationOptions);
                var id = TrainingOperation<LabeledFormModel>.GetTrainingOperationId(response);
                return new TrainingOperation<LabeledFormModel>(client._pipeline, id, client._options.SerializationOptions, client._labeledModelFactory);
            }
        }

        /// <summary>
        /// Get a <see cref="TrainingOperation{TModel}" /> status reference to an existhing training request.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="operationId">The operation id from a previous training request.</param>
        public static TrainingOperation<LabeledFormModel> StartTrainingWithLabelsX(this CustomFormClient client, string operationId)
        {
            return new TrainingOperation<LabeledFormModel>(client._pipeline, operationId, client._options.SerializationOptions, client._labeledModelFactory);
        }
    }
}