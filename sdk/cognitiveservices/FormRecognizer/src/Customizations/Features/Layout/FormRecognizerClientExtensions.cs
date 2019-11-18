using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public static partial class FormRecognizerClientExtensions
    {
        public static async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, Uri uri, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, uri, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }

        public static async Task<AnalyzeLayoutAsyncHeaders> StartAnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, Stream fileStream, AnalysisContentType contentType, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var resp = await operations.AnalyzeLayoutWithHttpMessagesAsync(language, fileStream, contentType, null, cancellationToken).ConfigureAwait(false))
            {
                return resp.Headers;
            }
        }
    }
}