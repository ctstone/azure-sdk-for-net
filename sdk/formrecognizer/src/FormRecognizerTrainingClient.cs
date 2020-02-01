// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.AI.FormRecognizer
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
            _formRecognizerClient = new FormRecognizerClient(endpoint, credential, new FormRecognizerClientOptions());
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        public virtual TrainCustomModelOperation StartTrainingCustomModel(string source, SourceFilter filter, CancellationToken cancellationToken = default)
        {
            var operation = _formRecognizerClient.StartTraining(new TrainingRequest()
            {
                Source = source,
                SourceFilter = filter
            });

            return new TrainCustomModelOperation(operation);
        }

        /// <summary>
        /// </summary>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        public virtual async Task<TrainCustomModelOperation> StartTrainingCustomModelAsync(string source, SourceFilter filter, CancellationToken cancellationToken = default)
        {
            var operation = await _formRecognizerClient.StartTrainingAsync(new TrainingRequest()
            {
                Source = source,
                SourceFilter = filter
            }, cancellationToken).ConfigureAwait(false);

            return new TrainCustomModelOperation(operation);
        }

        /// <summary>
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual TrainCustomModelOperation StartTrainingCustomModel(string operationId, CancellationToken cancellationToken = default)
        {
            var operation = _formRecognizerClient.StartTraining(operationId, cancellationToken);
            return new TrainCustomModelOperation(operation);
        }
    }
}
