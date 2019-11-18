using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Rest;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public partial class FormRecognizerClient
    {
        private const string Trace_AnalyzeLayout = "AnalyzeLayoutAsync";
        private const string Trace_GetAnalyzeLayoutResult = "GetAnalyzeLayoutResult";

        /// <summary>
        /// Analyze Layout
        /// </summary>
        /// <remarks>
        /// Extract text and layout information from a given document. The input
        /// document must be of one of the supported content types - 'application/pdf',
        /// 'image/jpeg', 'image/png' or 'image/tiff'. Alternatively, use
        /// 'application/json' type to specify the location (Uri or local path) of the
        /// document to be analyzed.
        /// </remarks>
        /// <param name='language'>
        /// The BCP-47 language code of the text to be detected in the image. Possible
        /// values include: 'en', 'es'
        /// </param>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="ErrorResponseException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="ValidationException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutWithHttpMessagesAsync(string language, Uri uri, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryParameters = new List<(string, string)>
            {
                ("language", language),
            };
            return AnalyzeWithHttpMessagesAsync<AnalyzeLayoutAsyncHeaders>(
                Trace_AnalyzeLayout,
                "layout/analyze",
                null,
                uri,
                null,
                null,
                queryParameters,
                customHeaders,
                cancellationToken);
        }

        public Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutWithHttpMessagesAsync(string language, Stream fileStream, AnalysisContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryParameters = new List<(string, string)>
            {
                ("language", language),
            };
            return AnalyzeWithHttpMessagesAsync<AnalyzeLayoutAsyncHeaders>(
                Trace_AnalyzeLayout,
                "layout/analyze",
                null,
                null,
                fileStream,
                contentType,
                queryParameters,
                customHeaders,
                cancellationToken);
        }
    }
}