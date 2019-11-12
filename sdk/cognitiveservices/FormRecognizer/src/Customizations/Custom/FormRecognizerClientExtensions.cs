// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;
    using System;
    using System.IO;

    /// <summary>
    /// Extension methods for FormRecognizerClient.
    /// </summary>
    public static partial class FormRecognizerClientExtensions
    {
        /// <summary>
        /// Train Custom Model
        /// </summary>
        /// <remarks>
        /// Create and train a custom model. The request must include a source
        /// parameter that is either an externally accessible Azure storage blob
        /// container Uri (preferably a Shared Access Signature Uri) or valid path to a
        /// data folder in a locally mounted drive. When local paths are specified,
        /// they must follow the Linux/Unix path format and be an absolute path rooted
        /// to the input mount configuration setting value e.g., if '{Mounts:Input}'
        /// configuration setting value is '/input' then a valid source path would be
        /// '/input/contosodataset'. All data to be trained is expected to be under the
        /// source folder or sub folders under it. Models are trained using documents
        /// that are of the following content type - 'application/pdf', 'image/jpeg',
        /// 'image/png', 'image/tiff'. Other type of content is ignored.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='trainRequest'>
        /// Training request parameters.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<TrainCustomModelAsyncHeaders> TrainCustomModelAsync(this IFormRecognizerClient operations, TrainRequest trainRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.TrainCustomModelWithHttpMessagesAsync(trainRequest, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Headers;
            }
        }

        /// <summary>
        /// List Custom Models
        /// </summary>
        /// <remarks>
        /// Get information about all custom models
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='op'>
        /// Specify whether to return summary or full list of models. Possible values
        /// include: 'full', 'summary'
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<ModelsModel> GetCustomModelsAsync(this IFormRecognizerClient operations, string op = default(string), CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetCustomModelsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

        /// <summary>
        /// List Custom Models
        /// </summary>
        /// <remarks>
        /// Get information about all custom models
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='op'>
        /// Specify whether to return summary or full list of models. Possible values
        /// include: 'full', 'summary'
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<ModelsModel> GetCustomModelsSummaryAsync(this IFormRecognizerClient operations, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetCustomModelsSummaryWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

        /// <summary>
        /// Get Custom Model
        /// </summary>
        /// <remarks>
        /// Get detailed information about a custom model.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='includeKeys'>
        /// Include list of extracted keys in model information.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<Model> GetCustomModelAsync(this IFormRecognizerClient operations, System.Guid modelId, bool? includeKeys = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetCustomModelWithHttpMessagesAsync(modelId, includeKeys, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

        /// <summary>
        /// Delete Custom Model
        /// </summary>
        /// <remarks>
        /// Mark model for deletion. Model artifacts will be permanently removed within
        /// a predetermined period.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task DeleteCustomModelAsync(this IFormRecognizerClient operations, System.Guid modelId, CancellationToken cancellationToken = default(CancellationToken))
        {
            (await operations.DeleteCustomModelWithHttpMessagesAsync(modelId, null, cancellationToken).ConfigureAwait(false)).Dispose();
        }

        /// <summary>
        /// Analyze Form
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location (Uri or
        /// local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='uri'>
        /// Remote URL to analyze.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeWithCustomModelHeaders> AnalyzeWithCustomModelAsyncAsync(this IFormRecognizerClient operations, System.Guid modelId, Uri uri, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, uri, includeTextDetails, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Headers;
            }
        }

        /// <summary>
        /// Analyze Form
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location (Uri or
        /// local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='contentType'>
        /// Content type of the file stream.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeWithCustomModelHeaders> AnalyzeWithCustomModelAsyncAsync(this IFormRecognizerClient operations, System.Guid modelId, Stream fileStream, AnalysisContentType contentType, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, fileStream, contentType, includeTextDetails, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Headers;
            }
        }

        /// <summary>
        /// Analyze Form
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// Alternatively, use 'application/json' type to specify the location (Uri or
        /// local path) of the document to be analyzed.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='includeTextDetails'>
        /// Include text lines and element references in the result.
        /// </param>
        /// <param name='byteArray'>
        /// .json, .pdf, .jpg, .png or .tiff type bytes.
        /// </param>
        /// <param name='contentType'>
        /// Content type of the bytes.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeWithCustomModelHeaders> AnalyzeWithCustomModelAsyncAsync(this IFormRecognizerClient operations, System.Guid modelId, byte[] byteArray, AnalysisContentType contentType, bool? includeTextDetails = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, byteArray, contentType, includeTextDetails, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Headers;
            }
        }

        /// <summary>
        /// Get Analyze Form Result
        /// </summary>
        /// <remarks>
        /// Obtain current status and the result of the analyze form operation.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='modelId'>
        /// Model identifier.
        /// </param>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeOperationResult> GetAnalyzeFormResultAsync(this IFormRecognizerClient operations, System.Guid modelId, System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetAnalyzeFormResultWithHttpMessagesAsync(modelId, resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }
    }
}
