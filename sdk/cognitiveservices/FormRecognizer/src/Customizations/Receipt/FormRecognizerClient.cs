// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Rest;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial class FormRecognizerClient : ServiceClient<FormRecognizerClient>, IFormRecognizerClient
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
                null,
                customHeaders,
                cancellationToken);
        }
        public Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptWithHttpMessagesAsync(Stream fileStream, AnalysisContentType contentTyep, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeWithHttpMessagesAsync<AnalyzeReceiptAsyncHeaders>(
                Trace_AnalyzeReceipt,
                "prebuilt/receipt/analyze",
                null,
                null,
                fileStream,
                null,
                contentTyep.ToString(),
                null,
                customHeaders,
                cancellationToken);
        }
        public Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptWithHttpMessagesAsync(byte[] byteArray, AnalysisContentType contentTyep, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeWithHttpMessagesAsync<AnalyzeReceiptAsyncHeaders>(
                Trace_AnalyzeReceipt,
                "prebuilt/receipt/analyze",
                null,
                null,
                null,
                byteArray,
                contentTyep.ToString(),
                null,
                customHeaders,
                cancellationToken);
        }

        /// <summary>
        /// Get Analyze Receipt Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze receipt operation.
        /// </remarks>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
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
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
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
        public Task<HttpOperationResponse<AnalyzeOperationResult>> GetAnalyzeReceiptResultWithHttpMessagesAsync(Guid resultId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAnalyzeResultWithHttpMessagesAsync(
                Trace_GetAnalyzeReceiptResult,
                "prebuilt/receipt/analyzeResults/{resultId}",
                new Dictionary<string, string>
                {
                    { "resultId", resultId.ToString() },
                },
                customHeaders,
                cancellationToken);

        }
    }
}
