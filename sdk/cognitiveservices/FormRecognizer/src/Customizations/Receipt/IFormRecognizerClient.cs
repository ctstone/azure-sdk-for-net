// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Rest;
    using Models;
    using System.IO;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial interface IFormRecognizerClient : System.IDisposable
    {

        /// <summary>
        /// Analyze Receipt
        /// </summary>
        /// <remarks>
        /// Extract field text and semantic values from a given receipt
        /// document. The input document must be of one of the supported
        /// content types - 'application/pdf', 'image/jpeg', 'image/png' or
        /// 'image/tiff'. Alternatively, use 'application/json' type to specify
        /// the location (Uri or local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptAsyncWithHttpMessagesAsync(string uri, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptAsyncWithHttpMessagesAsync(Stream fileStream, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpOperationHeaderResponse<AnalyzeReceiptAsyncHeaders>> AnalyzeReceiptAsyncWithHttpMessagesAsync(byte[] byteArray, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get Analyze Receipt Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze receipt
        /// operation.
        /// </remarks>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<AnalyzeOperationResult>> GetAnalyzeReceiptResultWithHttpMessagesAsync(System.Guid resultId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}
