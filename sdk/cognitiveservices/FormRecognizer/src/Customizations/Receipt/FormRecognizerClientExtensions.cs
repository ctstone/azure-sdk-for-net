// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;
    using System;
    using System.IO;

    /// <summary>
    /// Extension methods for FormRecognizerClient.
    /// </summary>
    public static partial class FormRecognizerClientExtensions
    {
        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(uri, null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, Stream fileStream, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, byte[] byteArray, CancellationToken cancellationToken = default(CancellationToken))
        {
            var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(byteArray, null, cancellationToken).ConfigureAwait(false);
            return resp.Headers;
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Uri uri, int retryTimes = 5, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(uri, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await operations.PollingResultAsync(guid, AnalyzeType.Receipt, retryTimes, cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Stream fileStream, int retryTimes = 5, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await operations.PollingResultAsync(guid, AnalyzeType.Receipt, retryTimes, cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, byte[] byteArray, int retryTimes = 5, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(byteArray, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var guid = GetGuid(header.OperationLocation);
                return await operations.PollingResultAsync(guid, AnalyzeType.Receipt, retryTimes, cancellationToken);
            }
        }

        /// <summary>
        /// Get Analyze Receipt Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze receipt operation.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<AnalyzeOperationResult> GetAnalyzeReceiptResultAsync(this IFormRecognizerClient operations, System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetAnalyzeReceiptResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
