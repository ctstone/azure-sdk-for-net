// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class SupervisedAnalysisOperation : Operation<SupervisedAnalysisResult>
    {
        private AnalyzeOperation _operation;

        /// <summary>
        /// </summary>
        /// <param name="operation"></param>
        internal SupervisedAnalysisOperation(AnalyzeOperation operation)
        {
            _operation = operation;
        }

        /// <summary>
        /// </summary>
        public override string Id => _operation.Id;

        /// <summary>
        /// </summary>
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
        public override SupervisedAnalysisResult Value => throw new NotImplementedException();
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations

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
        public override ValueTask<Response<SupervisedAnalysisResult>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
            => throw new NotImplementedException();

        /// <summary>
        /// </summary>
        /// <param name="pollingInterval"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<Response<SupervisedAnalysisResult>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default)
            => throw new NotImplementedException();
    }
}