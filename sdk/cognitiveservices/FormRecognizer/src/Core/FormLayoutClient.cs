// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Class to analyze form layout.
    /// </summary>
    public class FormLayoutClient : AnalysisClient
    {
        private const string LayoutPrefix = "/layout";

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class.
        /// </summary>
        internal FormLayoutClient(HttpPipeline pipeline, FormRecognizerClientOptions options)
            : base(pipeline, options, LayoutPrefix)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormLayoutClient"/> class.
        /// </summary>
        protected FormLayoutClient()
        { }
    }
}