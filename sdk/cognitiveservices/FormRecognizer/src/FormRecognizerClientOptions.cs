// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using System.Text.Json;
using Azure.AI.FormRecognizer.Serialization.Converters;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Set options for the Form Recognizer client.
    /// </summary>
    public class FormRecognizerClientOptions : ClientOptions
    {
        internal const ServiceVersion LatestVersion = ServiceVersion.V2_0_preview;

        /// <summary>
        /// The versions of Form Recognizer supported by this client library.
        /// </summary>
        public enum ServiceVersion
        {
#pragma warning disable CA1707 // Identifiers should not contain underscores
            /// <summary>Form Recognizer v2.0-preview</summary>
            V2_0_preview = 1,
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }

        /// <summary>
        /// Get the <see cref="ServiceVersion"/> of the service API used when making requests.
        /// </summary>
        public ServiceVersion Version { get; }

        /// <summary>
        /// Get the <see cref="JsonSerializerOptions" /> used by the client when processing JSON messages.
        /// </summary>
        public JsonSerializerOptions SerializationOptions { get; }

        /// <summary>
        /// Get the user agent string sent by the client to the service on each request.
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// Get the extra headers sent by the client to the service on each request.
        /// </summary>
        public HttpHeader[] ExtraHeaders { get; }

        internal Encoding Encoding { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClientOptions"/> class.
        /// </summary>
        /// <param name="version">Set the service version to use for all requests.</param>
        /// <param name="userAgent">Set the user agent string to send to the service for all requests.</param>
        /// <param name="extraHeaders">Set extra HTTP headers that will be sent to the service for all requests.</param>
        public FormRecognizerClientOptions(ServiceVersion version = LatestVersion, string userAgent = default, HttpHeader[] extraHeaders = default)
        {
            Version = version;
            UserAgent = userAgent;
            ExtraHeaders = extraHeaders;
            Encoding = Encoding.UTF8;
            SerializationOptions = new JsonSerializerOptions();
            // SerializationOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            // SerializationOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            // SerializationOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            SerializationOptions.Converters.Add(new AnalysisJsonConverter());
            SerializationOptions.Converters.Add(new ModelJsonConverter());
            SerializationOptions.Converters.Add(new TrainingRequestJsonConverter());
            SerializationOptions.Converters.Add(new AnalysisRequestJsonConverter());
            SerializationOptions.Converters.Add(new ErrorResponseJsonConverter());
        }

        internal string GetVersionString()
        {
            return Version switch
            {
                ServiceVersion.V2_0_preview => "v2.0-preview",
                _ => throw new NotSupportedException($"The service version {Version} is not supported."),
            };
        }
    }
}