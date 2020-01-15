// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Defines the synchronous and asynchronous operations to analyze forms and retrieve results.
    /// Supports analyzing files from both <see cref="Stream" /> and <see cref="Uri" /> objects.
    ///
    /// This client exposes default handling for the `includeTextDetails` query parameter.
    /// </summary>
    public class AnalyzeClient : AnalyzeClient<AnalyzeOptions>
    {
        private const string IncludeTextDetailsQueryKey = "includeTextDetails";
        private const string True = "true";
        private const string False = "false";

        /// <summary>
        /// /// Initializes a new instance of the <see cref="AnalyzeClient"/> class.
        /// </summary>
        protected AnalyzeClient()
        { }

        internal AnalyzeClient(HttpPipeline pipeline, FormRecognizerClientOptions options, string basePath)
            : base(pipeline, options, basePath)
        {
        }

        /// <summary>
        /// Apply option to include text details on an analyze request.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="request"></param>
        protected override void ApplyOptions(AnalyzeOptions options, Request request)
        {
            request.Uri.AppendQuery(IncludeTextDetailsQueryKey, options.IncludeTextDetails ? True : False);
        }
    }

    /// <summary>
    /// Options for the analyze operation.
    /// </summary>
    public struct AnalyzeOptions
    {
        /// <summary>
        /// Set to `true` to include text lines and element references in the result.
        /// </summary>
        public bool IncludeTextDetails { get; set; }

        /// <summary>
        /// Create options.
        /// </summary>
        /// <param name="includeTextDetails">Set to `true` to include text lines and element references in the result.</param>
        public AnalyzeOptions(bool includeTextDetails)
        {
            IncludeTextDetails = includeTextDetails;
        }

        /// <summary>
        /// Convert from Boolean.
        /// </summary>
        /// <param name="options">The options.</param>
        public static implicit operator bool(AnalyzeOptions options) => options.IncludeTextDetails;

        /// <summary>
        /// Convert to Boolean.
        /// </summary>
        /// <param name="boolValue">Boolean value.</param>
        public static implicit operator AnalyzeOptions(bool boolValue) => new AnalyzeOptions(boolValue);
    }
}