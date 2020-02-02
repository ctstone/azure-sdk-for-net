// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Serialization;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// Result of an analysis operation.
    /// </summary>
    internal class AnalysisResult
    {
        /// <summary>
        /// Version of schema used for this result.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Text extracted from the input.
        /// </summary>
        public ExtractedPageText[] ReadResults { get; internal set; }

        /// <summary>
        /// Page-level information extracted from the input.
        /// </summary>
        public PageResult[] PageResults { get; internal set; }

        /// <summary>
        /// Document-level information extracted from the input.
        /// </summary>
        public ExtractedLabeledFields[] DocumentResults { get; internal set; }

        /// <summary>
        /// List of errors reported during the analyze operation.
        /// </summary>
        public FormRecognizerError[] Errors { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisResult"/> class.
        /// </summary>
        protected AnalysisResult()
        { }

        internal static AnalysisResult Create() => new AnalysisResult();
    }
}