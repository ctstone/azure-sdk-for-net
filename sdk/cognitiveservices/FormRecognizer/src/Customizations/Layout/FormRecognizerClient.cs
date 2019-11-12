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
                DefaultContentType,
                queryParameters,
                customHeaders,
                cancellationToken);
        }

        public Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutWithHttpMessagesAsync(string language, Stream fileStream, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
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
                null,
                DefaultContentType,
                queryParameters,
                customHeaders,
                cancellationToken);
        }

        public Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutWithHttpMessagesAsync(string language, byte[] byteArray, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
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
                null,
                byteArray,
                DefaultContentType,
                queryParameters,
                customHeaders,
                cancellationToken);
        }

        /// <summary>
        /// Get Analyze Layout Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze layout operation
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
        public Task<HttpOperationResponse<AnalyzeOperationResult>> GetAnalyzeLayoutResultWithHttpMessagesAsync(Guid resultId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetAnalyzeResultWithHttpMessagesAsync(
                Trace_GetAnalyzeLayoutResult,
                "layout/analyzeResults/{resultId}",
                new Dictionary<string, string>
                {
                    { "resultId", resultId.ToString() },
                },
                customHeaders,
                cancellationToken);
        }
    }
}
