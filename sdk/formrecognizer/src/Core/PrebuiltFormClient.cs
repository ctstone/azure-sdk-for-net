// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// The prebuilt form client extracts information from prebuilt Form Recognizer models.
    /// </summary>
    public class PrebuiltFormClient : AnalyzeClient
    {
        internal const string PrebuiltBasePath = "/prebuilt";
        private readonly ReceiptClient _receipt;

        /// <summary>
        /// Use the prebuilt receipt-model client.
        /// /// </summary>
        public ReceiptClient Receipt => _receipt;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrebuiltFormClient"/> class.
        /// </summary>
        protected PrebuiltFormClient()
        { }

        internal PrebuiltFormClient(HttpPipeline pipeline, FormRecognizerClientOptions options)
        {
            _receipt = new ReceiptClient(pipeline, options);
        }

        internal static string GetModelPath(string modelName)
        {
            Throw.IfMissing(modelName, nameof(modelName));
            return $"{PrebuiltBasePath}/{modelName}";
        }
    }
}