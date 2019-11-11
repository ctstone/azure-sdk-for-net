// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial class FormRecognizerClient : ServiceClient<FormRecognizerClient>, IFormRecognizerClient
    {
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
        public async Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(string language, string uri, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await AnalyzeLayoutAsyncWithHttpMessagesAsync(ContentType.URI, language, uri, null, null, customHeaders, cancellationToken);
        }
        public async Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(string language, Stream fileStream, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await AnalyzeLayoutAsyncWithHttpMessagesAsync(ContentType.Stream, language, null, fileStream, null, customHeaders, cancellationToken);
        }
        public async Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(string language, byte[] byteArray, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await AnalyzeLayoutAsyncWithHttpMessagesAsync(ContentType.ByteArray, language, null, null, byteArray, customHeaders, cancellationToken);
        }
        private async Task<HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>> AnalyzeLayoutAsyncWithHttpMessagesAsync(ContentType contentType, string language, string uri = null, Stream fileStream = null, byte[] byteArray = null, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Endpoint == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "this.Endpoint");
            }
            if (language == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "language");
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("language", language);
                tracingParameters.Add("fileStream", fileStream);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "AnalyzeLayoutAsync", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri;
            var _url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "layout/analyze";
            _url = _url.Replace("{endpoint}", Endpoint);
            List<string> _queryParameters = new List<string>();
            if (language != null)
            {
                _queryParameters.Add(string.Format("language={0}", System.Uri.EscapeDataString(language)));
            }
            if (_queryParameters.Count > 0)
            {
                _url += "?" + string.Join("&", _queryParameters);
            }
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("POST");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


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

            // Serialize Request
            string _requestContent = null;
            if (!(fileStream == null && uri == null && byteArray == null))
            {
                switch (contentType)
                {
                    case (ContentType.URI):
                        var uriJson = JsonConvert.SerializeObject(new Dictionary<string, string> { { "url", uri } });
                        _requestContent = uriJson;
                        _httpRequest.Content = new StringContent(uriJson, System.Text.Encoding.UTF8, "application/json");
                        break;
                    case (ContentType.Stream):
                        var streamReader = new StreamReader(fileStream);
                        _requestContent = streamReader.ReadToEnd();
                        fileStream.Position = 0;
                        _httpRequest.Content = new StreamContent(fileStream);
                        _httpRequest.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                        break;
                    case (ContentType.ByteArray):
                        _httpRequest.Content = new ByteArrayContent(byteArray);
                        _httpRequest.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/octet-stream");
                        break;
                    default:
                        throw new System.ArgumentException("Unsupported content type.");
                }
            }
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
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
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
            var _result = new HttpOperationHeaderResponse<AnalyzeLayoutAsyncHeaders>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            try
            {
                _result.Headers = _httpResponse.GetHeadersAsJson().ToObject<AnalyzeLayoutAsyncHeaders>(JsonSerializer.Create(DeserializationSettings));
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
        public async Task<HttpOperationResponse<AnalyzeOperationResult>> GetAnalyzeLayoutResultWithHttpMessagesAsync(System.Guid resultId, Dictionary<string, List<string>> customHeaders = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (Endpoint == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "this.Endpoint");
            }
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace)
            {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
                tracingParameters.Add("resultId", resultId);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "GetAnalyzeLayoutResult", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri;
            var _url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + "layout/analyzeResults/{resultId}";
            _url = _url.Replace("{endpoint}", Endpoint);
            _url = _url.Replace("{resultId}", System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(resultId, SerializationSettings).Trim('"')));
            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new System.Uri(_url);
            // Set Headers


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

            // Serialize Request
            string _requestContent = null;
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
            if ((int)_statusCode != 200)
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
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
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
            var _result = new HttpOperationResponse<AnalyzeOperationResult>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int)_statusCode == 200)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    _result.Body = SafeJsonConvert.DeserializeObject<AnalyzeOperationResult>(_responseContent, DeserializationSettings);
                }
                catch (JsonException ex)
                {
                    _httpRequest.Dispose();
                    if (_httpResponse != null)
                    {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace)
            {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

    }
}
