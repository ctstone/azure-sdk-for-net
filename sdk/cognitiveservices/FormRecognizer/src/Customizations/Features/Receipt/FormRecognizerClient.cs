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
        private const string Trace_AnalyzeReceipt = "AnalyzeReceiptAsync";
        private const string Trace_GetAnalyzeReceiptResult = "GetAnalyzeReceiptResult";

        /// <summary>
        /// Analyze Receipt
        /// </summary>
        /// <remarks>
        /// Extract field text and semantic values from a given receipt document. The
        /// input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location (Uri or
        /// local path) of the document to be analyzed.
        /// </remarks>
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
        public Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptWithHttpMessagesAsync(Uri uri, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeWithHttpMessagesAsync<AnalyzeReceiptAsyncHeaders>(
                Trace_AnalyzeReceipt,
                "prebuilt/receipt/analyze",
                null,
                uri,
                null,
                null,
                null,
                customHeaders,
                cancellationToken);
        }
        public Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptWithHttpMessagesAsync(Stream fileStream, AnalysisContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeWithHttpMessagesAsync<AnalyzeReceiptAsyncHeaders>(
                Trace_AnalyzeReceipt,
                "prebuilt/receipt/analyze",
                null,
                null,
                fileStream,
                contentType,
                null,
                customHeaders,
                cancellationToken);
        }
    }
}