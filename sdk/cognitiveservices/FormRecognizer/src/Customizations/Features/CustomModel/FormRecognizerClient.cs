using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Rest;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public partial class FormRecognizerClient
    {
        private const string Trace_AnalyzeCustom = "AnalyzeWithCustomModel";
        private const string Trace_GetAnalyzeCustomResult = "GetAnalyzeFormResult";

        /// <summary>
        /// List Custom Models.
        /// </summary>
        /// <remarks>
        /// Get information about all custom models.
        /// </remarks>
        /// <param name='nextLink'>
        /// Provide the next link from a previous request to fetch the next page.
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
        public Task<HttpOperationResponse<ModelsModel>> GetCustomModelsWithHttpMessagesAsync(string nextLink = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var nextLinkToken = GetNextLinkToken(nextLink);
            return GetCustomModelsWithHttpMessagesAsync("full", nextLinkToken, customHeaders, cancellationToken);
        }

        /// <summary>
        /// Get summary of custom models.
        /// </summary>
        /// <remarks>
        /// Get information about all custom models
        /// </remarks>
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
        public Task<HttpOperationResponse<ModelsModel>> GetCustomModelsSummaryWithHttpMessagesAsync(Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return GetCustomModelsWithHttpMessagesAsync("summary", customHeaders, cancellationToken);
        }

        /// <summary>
        /// Analyze Form from URI.
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location (Uri or
        /// local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='uri'>
        /// URL to analyze.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
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
        public Task<HttpOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, Uri uri, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<(string, string)> queryParameters = null;

            if (includeTextDetails.HasValue)
            {
                queryParameters = new List<(string, string)>
                {
                    ("includeTextDetails", includeTextDetails.Value.ToString().ToLowerInvariant()),
                };
            }
            return AnalyzeWithHttpMessagesAsync<AnalyzeWithCustomModelHeaders>(
                Trace_AnalyzeCustom,
                "custom/models/{modelId}/analyze",
                new Dictionary<string, string>
                {
                    { "modelId", modelId.ToString() },
                },
                uri,
                null,
                null,
                queryParameters,
                customHeaders,
                cancellationToken);
        }

        /// <summary>
        /// Analyze Form from stream
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location (Uri or
        /// local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='contentType'>
        /// Content type of the stream.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
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
        public Task<HttpOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, Stream fileStream, AnalysisContentType contentType, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<(string, string)> queryParameters = null;

            if (includeTextDetails.HasValue)
            {
                queryParameters = new List<(string, string)>
                {
                    ("includeTextDetails", includeTextDetails.Value.ToString().ToLowerInvariant()),
                };
            }
            return AnalyzeWithHttpMessagesAsync<AnalyzeWithCustomModelHeaders>(
                Trace_AnalyzeCustom,
                "custom/models/{modelId}/analyze",
                new Dictionary<string, string>
                {
                    { "modelId", modelId.ToString() },
                },
                null,
                fileStream,
                contentType,
                queryParameters,
                customHeaders,
                cancellationToken);
        }

        private static string GetNextLinkToken(string nextLink)
        {
            if (string.IsNullOrEmpty(nextLink))
            {
                return null;
            }
            else
            {
                var parts = nextLink.Split(new[] { "nextLink=" }, StringSplitOptions.None);
                return parts.Length == 2 ? parts[1] : null;
            }
        }
    }
}