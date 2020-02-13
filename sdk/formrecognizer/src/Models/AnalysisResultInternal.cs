// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Serialization;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Result of an analysis operation.
    /// </summary>
    internal class AnalysisResultInternal
    {
        /// <summary>
        /// Version of schema used for this result.
        /// </summary>
        public string Version { get; internal set; }

        /// <summary>
        /// Text extracted from the input.
        /// </summary>
        public TextExtractionPage[] TextExtractionPages { get; internal set; }

        /// <summary>
        /// Page-level information extracted from the input.
        /// </summary>
        public FieldExtractionPageInternal[] FieldExtractionPages { get; internal set; }

        /// <summary>
        /// Document-level information extracted from the input.
        /// </summary>
        public PredefinedFormInternal[] PredefinedFieldExtractions { get; internal set; }

        /// <summary>
        /// List of errors reported during the analyze operation.
        /// </summary>
        public ErrorDetails[] Errors { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisResultInternal"/> class.
        /// </summary>
        protected AnalysisResultInternal()
        { }

        internal static AnalysisResultInternal Create() => new AnalysisResultInternal();
    }
}