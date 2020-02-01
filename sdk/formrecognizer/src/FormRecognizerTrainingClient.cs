// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.AI.FormRecognizer.Training
{
    /// <summary>
    /// </summary>
    public class FormRecognizerTrainingClient
    {
        private FormRecognizerClient _formRecognizerClient;

        /// <summary>
        /// </summary>
        protected FormRecognizerTrainingClient()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class using a key-based credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        public FormRecognizerTrainingClient(Uri endpoint, CognitiveKeyCredential credential)
            : this(endpoint, credential, new FormRecognizerTrainingClientOptions())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClient"/> class using a subscription key credential.
        /// </summary>
        /// <param name="endpoint">Endpoint.</param>
        /// <param name="credential">Your assigned subscription key, copied from https://portal.azure.com/</param>
        /// <param name="options">Optional service parameters.</param>
        public FormRecognizerTrainingClient(Uri endpoint, CognitiveKeyCredential credential, FormRecognizerTrainingClientOptions options)
        {
            var temp = options.Version;
            _formRecognizerClient = new FormRecognizerClient(endpoint, credential, new FormRecognizerClientOptions());
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        public virtual TrainUnsupervisedModelOperation StartTrainingUnsupervisedModel(string source, TrainingFileFilter filter, CancellationToken cancellationToken = default)
        {
            var operation = _formRecognizerClient.StartTraining(new TrainingRequest()
            {
                Source = source,
                SourceFilter = filter
            });

            return new TrainUnsupervisedModelOperation(operation);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        public virtual async Task<TrainUnsupervisedModelOperation> StartTrainingUnsupervisedModelAsync(string source, TrainingFileFilter filter, CancellationToken cancellationToken = default)
        {
            var operation = await _formRecognizerClient.StartTrainingAsync(new TrainingRequest()
            {
                Source = source,
                SourceFilter = filter
            }, cancellationToken).ConfigureAwait(false);

            return new TrainUnsupervisedModelOperation(operation);
        }

        /// <summary>
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual TrainUnsupervisedModelOperation StartTrainingUnsupervisedModel(string operationId, CancellationToken cancellationToken = default)
        {
            var operation = _formRecognizerClient.StartTraining(operationId, cancellationToken);
            return new TrainUnsupervisedModelOperation(operation);
        }


        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="labelFile"></param>
        /// <param name="cancellationToken"></param>
        public virtual TrainSupervisedModelOperation StartTrainingSupervisedModel(string source, TrainingFileFilter filter, string labelFile, CancellationToken cancellationToken = default)
        {
            var operation = _formRecognizerClient.StartTraining(new TrainingRequest()
            {
                Source = source,
                SourceFilter = filter,
                UseLabelFile = (labelFile != null) // TODO pass through.
            });

            return new TrainSupervisedModelOperation(operation);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="labelFile"></param>
        /// <param name="cancellationToken"></param>
        public virtual async Task<TrainSupervisedModelOperation> StartTrainingSupervisedModelAsync(string source, TrainingFileFilter filter, string labelFile, CancellationToken cancellationToken = default)
        {
            var operation = await _formRecognizerClient.StartTrainingAsync(new TrainingRequest()
            {
                Source = source,
                SourceFilter = filter,
                UseLabelFile = (labelFile != null) // TODO pass through.
            }, cancellationToken).ConfigureAwait(false);

            return new TrainSupervisedModelOperation(operation);
        }

        /// <summary>
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual TrainSupervisedModelOperation StartTrainingSupervisedModel(string operationId, CancellationToken cancellationToken = default)
        {
            var operation = _formRecognizerClient.StartTraining(operationId, cancellationToken);
            return new TrainSupervisedModelOperation(operation);
        }
    }
}
