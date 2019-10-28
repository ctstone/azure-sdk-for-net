// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public partial class FormRecognizerClient : ServiceClient<FormRecognizerClient>, IFormRecognizerClient
    {
        /// <summary>
        /// Initializes a new instance of the FormRecognizerClient class.
        /// </summary>
        /// <param name="apiKey">The API subscription key for the Form Recognizer service</param>
        /// <param name="endpoint">The base endpoint of the service, e.g https://eastus.api.cognitive.microsoft.com/</param>
        public FormRecognizerClient(string apiKey, string endpoint)
            : this(new FormClientCredentials(apiKey))
        {
            Endpoint = endpoint;
        }

        /// <summary>
        /// Analyze Form from remote URL
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// </remarks>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="fileStream">Stream of file contents.</param>
        /// <param name="contentType">Content type of the file stream.</param>
        /// <param name="includeTextDetails"></param>
        /// <param name='customHeaders'>The headers that will be added to request. </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<AzureOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, AnalyzeRemoteFormRequest request, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeWithCustomModelWithHttpMessagesAsync(modelId, includeTextDetails, request, customHeaders, cancellationToken);
        }

        /// <summary>
        /// Analyze Form from local file stream
        /// </summary>
        /// <remarks>
        /// Extract key-value pairs, tables, and semantic values from a given document.
        /// The input document must be of one of the supported content types -
        /// 'application/pdf', 'image/jpeg', 'image/png' or 'image/tiff'.
        /// </remarks>
        /// <param name="modelId">Model identifier.</param>
        /// <param name="fileStream">Stream of file contents.</param>
        /// <param name="contentType">Content type of the file stream.</param>
        /// <param name="includeTextDetails"></param>
        /// <param name='customHeaders'>The headers that will be added to request. </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<AzureOperationHeaderResponse<AnalyzeWithCustomModelHeaders>> AnalyzeWithCustomModelWithHttpMessagesAsync(Guid modelId, Stream fileStream, FormContentType contentType, bool? includeTextDetails = false, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var url = "custom/models/{modelId}/analyze"
                .Replace("{modelId}", Uri.EscapeDataString(SafeJsonConvert.SerializeObject(modelId, SerializationSettings).Trim('"')));
            IDictionary<string, object> tracingParameters = null;
            IList<string> queryParameters = null;
            string tracingMethod = null;
            if (ServiceClientTracing.IsEnabled)
            {
                tracingMethod = "AnalyzeWithCustomModel";
                tracingParameters = new Dictionary<string, object>
                {
                    { "includeTextDetail", includeTextDetails },
                    { "modelId", modelId },
                };
            }
            if (includeTextDetails != null)
            {
                queryParameters = new List<string>();
                queryParameters.Add(string.Format("includeTextDetails={0}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(includeTextDetails, SerializationSettings).Trim('"'))));
            }
            return AnalyzeWithHttpMessagesAsync<AnalyzeWithCustomModelHeaders>(url, fileStream, contentType, queryParameters, customHeaders, tracingMethod, tracingParameters, cancellationToken);
        }

        public Task<AzureOperationHeaderResponse<AnalyzeReceiptHeaders>> AnalyzeReceiptWithHttpMessagesAsync(AnalyzeRemoteFormRequest request, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeReceiptWithHttpMessagesAsync(request as object, customHeaders, cancellationToken);
        }

        public Task<AzureOperationHeaderResponse<AnalyzeReceiptHeaders>> AnalyzeReceiptWithHttpMessagesAsync(Stream fileStream, FormContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var url = "prebuilt/receipt/analyze";
            string tracingMethod = ServiceClientTracing.IsEnabled
                ? "AnalyzeReceipt"
                : null;

            return AnalyzeWithHttpMessagesAsync<AnalyzeReceiptHeaders>(url, fileStream, contentType, null, customHeaders, tracingMethod, null, cancellationToken);
        }

        public Task<AzureOperationHeaderResponse<AnalyzeLayoutHeaders>> AnalyzeLayoutWithHttpMessagesAsync(AnalyzeRemoteFormRequest request, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return AnalyzeLayoutWithHttpMessagesAsync(request as object, customHeaders, cancellationToken);
        }

        public Task<AzureOperationHeaderResponse<AnalyzeLayoutHeaders>> AnalyzeLayoutWithHttpMessagesAsync(Stream fileStream, FormContentType contentType, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var url = "prebuilt/receipt/analyze";
            string tracingMethod = ServiceClientTracing.IsEnabled
                ? "AnalyzeLayout"
                : null;

            return AnalyzeWithHttpMessagesAsync<AnalyzeLayoutHeaders>(url, fileStream, contentType, null, customHeaders, tracingMethod, null, cancellationToken);
        }

        private async Task<AzureOperationHeaderResponse<THeader>> AnalyzeWithHttpMessagesAsync<THeader>(
            string url,
            Stream fileStream,
            FormContentType contentType,
            IList<string> queryParameters = null,
            IDictionary<string, List<string>> customHeaders = null,
            string tracingMethod = null,
            IDictionary<string, object> tracingParameters = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Endpoint == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "this.Endpoint");
            }
            if (fileStream == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(fileStream));
            }
            if (contentType == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, nameof(contentType));
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                tracingParameters = tracingParameters ?? new Dictionary<string, object>();
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                tracingParameters.Add("fileStream", fileStream);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, tracingMethod, tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri;
            var _url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + url;
            _url = _url.Replace("{endpoint}", Endpoint);

            if (queryParameters != null && queryParameters.Count > 0)
            {
                _url += (_url.Contains("?") ? "&" : "?") + string.Join("&", queryParameters);
            }
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("POST");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers
            if (GenerateClientRequestId != null && GenerateClientRequestId.Value)
            {
                _httpRequest.Headers.TryAddWithoutValidation("x-ms-client-request-id", System.Guid.NewGuid().ToString());
            }
            if (AcceptLanguage != null)
            {
                if (_httpRequest.Headers.Contains("accept-language"))
                {
                    _httpRequest.Headers.Remove("accept-language");
                }
                _httpRequest.Headers.TryAddWithoutValidation("accept-language", AcceptLanguage);
            }


            if (customHeaders != null)
            {
                foreach (var _header in customHeaders)
                {
                    if (_httpRequest.Headers.Contains(_header.Key))
                    {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Write Request
            _httpRequest.Content = new StreamContent(fileStream);
            _httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType.ContentType);

            // Set Credentials
            if (Credentials != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            }
            // Send Request
            if (_shouldTrace)
            {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace)
            {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int)_statusCode != 202)
            {
                var ex = new ErrorResponseException(string.Format("Operation returned an invalid status code '{0}'", _statusCode));
                try
                {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    ErrorResponse _errorBody = SafeJsonConvert.DeserializeObject<ErrorResponse>(_responseContent, DeserializationSettings);
                    if (_errorBody != null)
                    {
                        ex.Body = _errorBody;
                    }
                }
                catch (JsonException)
                {
                    // Ignore the exception
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, string.Empty);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace)
                {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new AzureOperationHeaderResponse<THeader>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            if (_httpResponse.Headers.Contains("x-ms-request-id"))
            {
                _result.RequestId = _httpResponse.Headers.GetValues("x-ms-request-id").FirstOrDefault();
            }
            try
            {
                _result.Headers = _httpResponse.GetHeadersAsJson().ToObject<THeader>(JsonSerializer.Create(DeserializationSettings));
            }
            catch (JsonException ex)
            {
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }
                throw new SerializationException("Unable to deserialize the headers.", _httpResponse.GetHeadersAsJson().ToString(), ex);
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }
    }
}