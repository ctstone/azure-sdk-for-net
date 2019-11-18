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
        /// List Custom Models with pagination.
        /// </summary>
        /// <remarks>
        /// Get information about all custom models
        /// </remarks>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<ModelsModel>> GetCustomModelsWithHttpMessagesAsync(string nextLink = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get summary of custom models.
        /// </summary>
        /// <remarks>
        /// Get information about all custom models.
        /// </remarks>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationResponse<ModelsModel>> GetCustomModelsSummaryWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Analyze Form from stream
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given
        /// document. The input document must be of one of the supported
        /// content types - 'application/pdf', 'image/jpeg', 'image/png' or
        /// 'image/tiff'. Alternatively, use 'application/json' type to specify
        /// the location (Uri or local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='fileStream'>
        /// Stream to analyze.
        /// </param>
        /// <param name='contentType'>
        /// Content-Type of the stream.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, Stream fileStream, AnalysisContentType contentType, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Analyze Form from URI
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given
        /// document. The input document must be of one of the supported
        /// content types - 'application/pdf', 'image/jpeg', 'image/png' or
        /// 'image/tiff'. Alternatively, use 'application/json' type to specify
        /// the location (Uri or local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='uri'>
        /// Remote URL to analyze.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        Task<HttpOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, Uri uri, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken));
    }
}