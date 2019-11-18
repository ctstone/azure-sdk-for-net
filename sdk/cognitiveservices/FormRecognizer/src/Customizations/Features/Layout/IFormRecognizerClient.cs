using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Rest;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public partial interface IFormRecognizerClient
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
        Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutWithHttpMessagesAsync(string language, Uri uri, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutWithHttpMessagesAsync(string language, Stream fileStream, AnalysisContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}