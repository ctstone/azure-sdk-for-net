// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.

using Microsoft.Rest;

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
    }
}