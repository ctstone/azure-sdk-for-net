// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// The prebuilt form client extracts information from prebuilt Form Recognizer models.
    /// </summary>
    public abstract class PrebuiltFormClient : AnalysisClient<AnalyzeOptions>
    {
        internal const string PrebuiltBasePath = "/prebuilt";
        private readonly string _prebuiltName;

        /// <summary>
        /// Prebuilt name.
        /// </summary>
        protected string PrebuiltName => _prebuiltName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrebuiltFormClient"/> class.
        /// </summary>
        protected PrebuiltFormClient()
        { }

        internal PrebuiltFormClient(string prebuiltName, HttpPipeline pipeline, FormRecognizerClientOptions options)
            : base(pipeline, options, GetModelPath(prebuiltName))
        {
            _prebuiltName = prebuiltName;
        }

        internal static string GetModelPath(string modelName)
        {
            Throw.IfMissing(modelName, nameof(modelName));
            return $"{PrebuiltBasePath}/{modelName}";
        }

        /// <summary>
        /// Apply options to an analyze request.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="request"></param>
        protected override void ApplyOptions(AnalyzeOptions options, Request request)
        {
            ApplyAnalyzeOptions(options, request);
        }
    }
}