// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Custom;
using Azure.AI.FormRecognizer.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.AI.FormRecognizer.Training
{
    /// <summary>
    /// </summary>
    public class TrainUnsupervisedModelOperation : Operation<UnsupervisedTrainingResult>
    {
        private TrainingOperation _operation;

        internal TrainUnsupervisedModelOperation(TrainingOperation operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// </summary>
        public override string Id => _operation.Id;

        /// <summary>
        /// </summary>
        public override UnsupervisedTrainingResult Value => _operation.Value;

        /// <summary>
        /// </summary>
        public override bool HasCompleted => _operation.HasCompleted;

        /// <summary>
        /// </summary>
        public override bool HasValue => _operation.HasValue;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override Response GetRawResponse() => _operation.GetRawResponse();

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
            => _operation.UpdateStatus(cancellationToken);

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
            => _operation.UpdateStatusAsync(cancellationToken);

        /// <summary>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<Response<UnsupervisedTrainingResult>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
            => _operation.WaitForCompletionAsync(cancellationToken);

        /// <summary>
        /// </summary>
        /// <param name="pollingInterval"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<Response<UnsupervisedTrainingResult>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default)
            => _operation.WaitForCompletionAsync(pollingInterval, cancellationToken);
    }
}