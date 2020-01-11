// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Hello World
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
        /// Get the Gets the <see cref="ServiceVersion"/> of the service API used when making requests.
        /// </summary>
        public ServiceVersion Version { get; }

        /// <summary>
        /// Serialization options.
        /// </summary>
        public JsonSerializerOptions SerializationOptions { get; }

        /// <summary>
        /// User agent.
        /// </summary>
        public string UserAgent { get; }

        /// <summary>
        /// Extra headers.
        /// </summary>
        public HttpHeader[] ExtraHeaders { get; }

        /// <summary>
        /// Text encoding.
        /// </summary>
        internal Encoding Encoding { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClientOptions"/> class.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="userAgent"></param>
        /// <param name="extraHeaders"></param>
        public FormRecognizerClientOptions(ServiceVersion version = LatestVersion, string userAgent = default, HttpHeader[] extraHeaders = default)
        {
            Version = version;
            UserAgent = userAgent;
            ExtraHeaders = extraHeaders;
            Encoding = Encoding.UTF8;
            SerializationOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            SerializationOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            SerializationOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
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