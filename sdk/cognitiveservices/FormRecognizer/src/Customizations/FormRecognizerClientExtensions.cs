using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public static partial class FormRecognizerClientExtensions
    {
        public static async Task<AnalyzeWithCustomModelHeaders> AnalyzeWithCustomModelAsync(this IFormRecognizerClient operations, Guid modelId, Stream fileStream, FormContentType contentType, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, fileStream, contentType, includeTextDetails, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Headers;
            }
        }

        public static async Task<AnalyzeWithCustomModelHeaders> AnalyzeWithCustomModelAsync(this IFormRecognizerClient operations, Guid modelId, AnalyzeRemoteFormRequest request, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, request, includeTextDetails, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Headers;
            }
        }

        public static async Task<AnalyzeReceiptHeaders> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Guid modelId, Stream fileStream, FormContentType contentType, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.AnalyzeReceiptWithHttpMessagesAsync(fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Headers;
            }
        }

        public static async Task<AnalyzeReceiptHeaders> AnalyzeReceiptAsync(this IFormRecognizerClient operations, Guid modelId, AnalyzeRemoteFormRequest request, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.AnalyzeReceiptWithHttpMessagesAsync(request, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Headers;
            }
        }

        public static async Task<AnalyzeLayoutHeaders> AnalyzeLayoutAsync(this IFormRecognizerClient operations, Guid modelId, Stream fileStream, FormContentType contentType, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.AnalyzeLayoutWithHttpMessagesAsync(fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Headers;
            }
        }

        public static async Task<AnalyzeLayoutHeaders> AnalyzeLayoutAsync(this IFormRecognizerClient operations, Guid modelId, AnalyzeRemoteFormRequest request, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var result = await operations.AnalyzeLayoutWithHttpMessagesAsync(request, null, cancellationToken).ConfigureAwait(false))
            {
                return result.Headers;
            }
        }
    }
}