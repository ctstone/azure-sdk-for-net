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
        /// Analyze Layout
        /// </summary>
        /// <remarks>
        /// Extract text and layout information from a given document. The
        /// input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location
        /// (Uri or local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='language'>
        /// The BCP-47 language code of the text to be detected in the image.
        /// Possible values include: 'en', 'es'
        /// </param>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(string language, string uri, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(string language, Stream fileStream, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(string language, byte[] byteArray, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get Analyze Layout Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze layout
        /// operation
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
        Task<HttpOperationResponse<AnalyzeOperationResult>> GetAnalyzeLayoutResultWithHttpMessagesAsync(System.Guid resultId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

    }
}
