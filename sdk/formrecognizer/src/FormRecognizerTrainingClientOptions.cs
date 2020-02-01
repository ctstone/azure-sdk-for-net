// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Training
{
    /// <summary>
    /// </summary>
    public class FormRecognizerTrainingClientOptions
    {
        internal const ServiceVersion LatestVersion = ServiceVersion.V2_0_Preview;

        /// <summary>
        /// The versions of Form Recognizer supported by this client library.
        /// </summary>
        public enum ServiceVersion
        {
#pragma warning disable CA1707 // Identifiers should not contain underscores
            /// <summary>Form Recognizer v2.0-preview</summary>
            V2_0_Preview = 1,
#pragma warning restore CA1707 // Identifiers should not contain underscores
        }

        /// <summary>
        /// Get the <see cref="ServiceVersion"/> of the service API used when making requests.
        /// </summary>
        public ServiceVersion Version { get; }

        /// <summary>
        /// Get the extra headers sent by the client to the service on each request.
        /// </summary>

        internal JsonSerializerOptions SerializationOptions { get; }
        internal Encoding Encoding { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormRecognizerTrainingClientOptions"/> class.
        /// </summary>
        /// <param name="version">Set the service version to use for all requests.</param>
        public FormRecognizerTrainingClientOptions(ServiceVersion version = LatestVersion)
        {
            Version = version;
            Encoding = Encoding.UTF8;
            SerializationOptions = new JsonSerializerOptions();
            FormRecognizerClientOptions.AddJsonConverters(SerializationOptions);
        }

        internal static string GetVersionString(ServiceVersion version)
        {
            return version switch
            {
                ServiceVersion.V2_0_Preview => "v2.0-preview",
                _ => throw new NotSupportedException($"The service version {version} is not supported."),
            };
        }
    }
}