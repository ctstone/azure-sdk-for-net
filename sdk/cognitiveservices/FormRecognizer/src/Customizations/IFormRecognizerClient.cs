// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Microsoft.Rest;
    using Models;
    using Newtonsoft.Json;
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extracts information from forms and images into structured data.
    /// </summary>
    public partial interface IFormRecognizerClient : System.IDisposable
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }

        /// <summary>
        /// Supported Cognitive Services endpoints (protocol and hostname, for
        /// example: https://westus2.api.cognitive.microsoft.com).
        /// </summary>
        string Endpoint { get; set; }

        /// <summary>
        /// Subscription credentials which uniquely identify client
        /// subscription.
        /// </summary>
        ServiceClientCredentials Credentials { get; }
    }
}
