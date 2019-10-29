// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Models;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;
    using System;

    /// <summary>
    /// Extension methods for FormRecognizerClient.
    /// </summary>
    public static partial class FormRecognizerClientExtensions
    {

        public static async Task<AnalyzeOperationResult> AnalyzeWithCustomModelAsync(this IFormRecognizerClient operations, System.Guid modelId, bool? includeTextDetails = false, object fileStream = default(object), CancellationToken cancellationToken = default(CancellationToken))
        {            
            var header = await AnalyzeWithCustomModelAsyncAsync(operations, modelId, includeTextDetails, fileStream, cancellationToken);
            var match = Regex.Match(header.OperationLocation, @"([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
            if (match.Success)
            {
                int retryTimeframe = 1;
                for (int retryCount = 5; retryCount > 0; retryCount--)
                {
                    var body = await GetAnalyzeFormResultAsync(operations, modelId, new Guid(match.Groups[2].ToString()), cancellationToken);
                    if (body.Status.ToSerializedValue() == "succeeded ")
                    {
                        return body;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(retryTimeframe));
                    retryTimeframe *= 2;
                }
                throw new ErrorResponseException("Timeout.");
            }
            throw new ArgumentException("Invalid URL.");
        }

        public static async Task<AnalyzeOperationResult> AnalyzeReceiptAsync(this IFormRecognizerClient operations, object fileStream = default(object), CancellationToken cancellationToken = default(CancellationToken))
        {
            var header = await AnalyzeReceiptAsyncAsync(operations, fileStream, cancellationToken);
            var match = Regex.Match(header.OperationLocation, @"([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
            if (match.Success)
            {
                int retryTimeframe = 1;
                for (int retryCount = 5; retryCount > 0; retryCount--)
                {
                    var body = await GetAnalyzeReceiptResultAsync(operations, new Guid(match.Groups[1].ToString()), cancellationToken);
                    if (body.Status.ToSerializedValue() == "succeeded")
                    {
                        return body;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(retryTimeframe));
                    retryTimeframe *= 2;
                }
                throw new ErrorResponseException("Timeout.");
            }
            throw new ArgumentException("Invalid URL.");
        }

        public static async Task<AnalyzeOperationResult> AnalyzeLayoutAsync(this IFormRecognizerClient operations, string language, object fileStream = default(object), CancellationToken cancellationToken = default(CancellationToken))
        { 
            var header = await AnalyzeLayoutAsyncAsync(operations, language, fileStream, cancellationToken);
            var match = Regex.Match(header.OperationLocation, @"([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
            if (match.Success)
            {
                int retryTimeframe = 1;
                for (int retryCount = 5; retryCount > 0; retryCount--)
                {
                    var body = await GetAnalyzeLayoutResultAsync(operations, new Guid(match.Groups[1].ToString()), cancellationToken);
                    if (body.Status.ToSerializedValue() == "succeeded ")
                    {
                        return body;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(retryTimeframe));
                    retryTimeframe *= 2;
                }
                throw new ErrorResponseException("Timeout.");
            }
            throw new ArgumentException("Invalid URL.");
        }
       
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


        public static async Task<TrainCustomModelAsyncHeaders> TrainCustomModelAsyncAsync(this IFormRecognizerClient operations, TrainRequest trainRequest, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.TrainCustomModelAsyncWithHttpMessagesAsync(trainRequest, null, cancellationToken).ConfigureAwait(false))
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
            using (var _result = await operations.GetCustomModelsWithHttpMessagesAsync(op, null, cancellationToken).ConfigureAwait(false))
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
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeWithCustomModelHeaders> AnalyzeWithCustomModelAsyncAsync(this IFormRecognizerClient operations, System.Guid modelId, bool? includeTextDetails = false, object fileStream = default(object), CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, includeTextDetails, fileStream, null, cancellationToken).ConfigureAwait(false))
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
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        public static async Task<AnalyzeReceiptAsyncHeaders> AnalyzeReceiptAsyncAsync(this IFormRecognizerClient operations, object fileStream = default(object), CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeReceiptAsyncWithHttpMessagesAsync(fileStream, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Headers;
            }
        }

        /// <summary>
        /// Get Analyze Receipt Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze receipt operation.
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeOperationResult> GetAnalyzeReceiptResultAsync(this IFormRecognizerClient operations, System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetAnalyzeReceiptResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

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
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='language'>
        /// The BCP-47 language code of the text to be detected in the image. Possible
        /// values include: 'en', 'es'
        /// </param>
        /// <param name='fileStream'>
        /// .json, .pdf, .jpg, .png or .tiff type file stream.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeLayoutAsyncHeaders> AnalyzeLayoutAsyncAsync(this IFormRecognizerClient operations, string language, object fileStream = default(object), CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.AnalyzeLayoutAsyncWithHttpMessagesAsync(language, fileStream, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Headers;
            }
        }

        /// <summary>
        /// Get Analyze Layout Result
        /// </summary>
        /// <remarks>
        /// Track the progress and obtain the result of the analyze layout operation
        /// </remarks>
        /// <param name='operations'>
        /// The operations group for this extension method.
        /// </param>
        /// <param name='resultId'>
        /// Analyze operation result identifier.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        private static async Task<AnalyzeOperationResult> GetAnalyzeLayoutResultAsync(this IFormRecognizerClient operations, System.Guid resultId, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var _result = await operations.GetAnalyzeLayoutResultWithHttpMessagesAsync(resultId, null, cancellationToken).ConfigureAwait(false))
            {
                return _result.Body;
            }
        }

    }
}
