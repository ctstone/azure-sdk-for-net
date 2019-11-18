using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public static partial class FormRecognizerClientExtensions
    {
        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(uri, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeReceiptAsyncHeaders> StartAnalyzeReceiptAsync(this IFormRecognizerClient operations, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(uri, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeReceiptResultAsync(operationId, ct), cancellationToken);
            }
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                var header = _result.Headers;
                var operationId = GetOperationId(header.OperationLocation);
                return await operations.WaitForOperation((ct) => operations.GetAnalyzeReceiptResultAsync(operationId, ct), cancellationToken);
            }
        }
    }
}