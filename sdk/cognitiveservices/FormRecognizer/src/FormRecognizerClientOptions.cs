// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using System.Text.Json;
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
        /// Text encoding.
        /// </summary>
        internal Encoding Encoding { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerClientOptions"/> class.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="serializationOptions"></param>
        /// <param name="userAgent"></param>
        public FormRecognizerClientOptions(ServiceVersion version = LatestVersion, JsonSerializerOptions serializationOptions = default, string userAgent = default)
        {
            Version = version;
            SerializationOptions = serializationOptions;
            UserAgent = userAgent;
            Encoding = Encoding.UTF8;
        }

        internal string GetVersionString()
        {
            return Version switch
            {
                ServiceVersion.V2_0_preview => "2.0-preview",
                _ => throw new NotSupportedException($"The service version {Version} is not supported."),
            };
        }
    }
}