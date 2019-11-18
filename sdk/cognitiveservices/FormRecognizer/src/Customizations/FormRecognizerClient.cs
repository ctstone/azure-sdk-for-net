using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public partial class FormRecognizerClient
    {
        public FormRecognizerClient(string apiKey, string endpoint)
        {
            if (apiKey == null)
            {
                throw new ArgumentNullException(nameof(apiKey));
            }
            if (endpoint == null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }
            Credentials = new FormClientCredentials(apiKey);
            Endpoint = endpoint;
            Initialize();
        }

        private async Task<HttpOperationHeaderResponse<T>> AnalyzeWithHttpMessagesAsync<T>(
            string tracingMethod,
            string urlPath,
            Dictionary<string, string> urlParameters = null,
            Uri uri = null,
            Stream fileStream = null,
            AnalysisContentType contentType = null,
            List<(string, string)> queryParameters = null,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken))
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
                if (urlParameters != null)
                {
                    foreach (var kvp in urlParameters)
                    {
                        tracingParameters.Add(kvp.Key, kvp.Value);
                    }
                }
                if (queryParameters != null)
                {
                    foreach (var (key, value) in queryParameters)
                    {
                        tracingParameters.Add(key, value);
                    }
                }
                if (fileStream != null)
                {
                    tracingParameters.Add("fileStream", fileStream);
                }
                if (uri != null)
                {
                    tracingParameters.Add("uri", uri);
                }
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, tracingMethod, tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri;
            var _url = _baseUrl + (_baseUrl.EndsWith("/") ? "" : "/") + urlPath;
            _url = _url.Replace("{endpoint}", Endpoint);
            if (urlParameters != null)
            {
                foreach (var kvp in urlParameters)
                {
                    _url = _url.Replace('{' + kvp.Key + '}', System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(kvp.Value, SerializationSettings).Trim('"')));
                }
            }
            if (queryParameters != null && queryParameters.Count > 0)
            {
                for (var i = 0; i < queryParameters.Count; i += 1)
                {
                    var (key, value) = queryParameters[i];
                    _url += i == 0 ? "?" : "&";
                    _url += key + '=' + value;
                }
            }

            // Create HTTP transport objects
            var _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("POST");
            _httpRequest.RequestUri = new System.Uri(_url);

            // Because we are sending an arbitrarily large POST payload, and the server may reject large payloads,
            // instruct the client to wait for 100-continue from the server before writing the data. Without this,
            // the client will receive an IO Exception for a broken pipe during a naive write of a large payload
            _httpRequest.Headers.ExpectContinue = true;

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
            if (uri != null)
            {
                var request = new AnalyzeUrlRequest { Source = uri };
                var json = JsonConvert.SerializeObject(request, SerializationSettings);
                _httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
                _requestContent = json;
            }
            else if (fileStream != null)
            {
                _httpRequest.Content = new StreamContent(fileStream);
                _requestContent = fileStream.GetType().Name;
                if (contentType != null)
                {
                    _httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType.ToString());
                }
            }
            else
            {
                throw new ArgumentException("Unsupported content type.");
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
            var _result = new HttpOperationHeaderResponse<T>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            try
            {
                _result.Headers = _httpResponse.GetHeadersAsJson().ToObject<T>(JsonSerializer.Create(DeserializationSettings));
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