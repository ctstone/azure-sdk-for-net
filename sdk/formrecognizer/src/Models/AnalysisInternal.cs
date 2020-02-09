// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;
using Azure.AI.FormRecognizer.Serialization.Converters;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Status and result of an analysis operation.
    /// </summary>
    internal class AnalysisInternal
    {
        /// <summary>
        /// Status of the operation.
        /// </summary>
        public OperationStatus Status { get; internal set; }

        /// <summary>
        /// Date and time when the analysis operation was submitted.
        /// </summary>
        public DateTimeOffset CreatedOn { get; internal set; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedOn { get; internal set; }

        /// <summary>
        /// Results of the analyze operation.
        /// </summary>
        public AnalysisResultInternal AnalyzeResult { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisInternal"/> class.
        /// </summary>
        protected AnalysisInternal()
        { }

        internal static AnalysisInternal Create() => new AnalysisInternal();
    }
}