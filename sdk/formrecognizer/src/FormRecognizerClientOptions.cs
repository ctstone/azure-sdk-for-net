// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
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
        /// Initializes a new instance of the <see cref="FormRecognizerClientOptions"/> class.
        /// </summary>
        /// <param name="version">Set the service version to use for all requests.</param>
        public FormRecognizerClientOptions(ServiceVersion version = LatestVersion)
        {
            Version = version;
            Encoding = Encoding.UTF8;
            SerializationOptions = new JsonSerializerOptions();
            SerializationOptions.Converters.Add(new AnalysisJsonConverter());
            SerializationOptions.Converters.Add(new ModelJsonConverter());
            SerializationOptions.Converters.Add(new TrainingRequestJsonConverter());
            SerializationOptions.Converters.Add(new AnalysisRequestJsonConverter());
            SerializationOptions.Converters.Add(new ErrorResponseJsonConverter());
            SerializationOptions.Converters.Add(new ModelListingJsonConverter());
        }
    }
}