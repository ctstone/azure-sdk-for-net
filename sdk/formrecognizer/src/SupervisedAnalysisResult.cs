// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Models;
using System;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class SupervisedAnalysisResult
    {
        /// <summary>
        /// Status of the operation.
        /// </summary>
        internal AnalysisStatus Status { get; set; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        internal DateTimeOffset LastUpdateTime { get; set; }

        // TODO: do we need these timestamps or status for any customer scenario?

        /// <summary>
        /// Date and time when the analysis operation was submitted.
        /// </summary>
        internal DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// Information extracted from each form in the input file.
        /// </summary>
        public SupervisedExtractedFields[] FormValues { get; internal set; }

        // TODO: values is by form, would it make sense to have tables by form as well?
        // Would it make sense to have tables inside of FormValues, or otherwise grouped
        // together by form?

        /// <summary>
        /// List of data tables extracted from the page.
        /// </summary>
        public ExtractedTable[] FormTables { get; internal set; }

        /// <summary>
        /// Raw output of the Optical Character Recognition engine, including text
        /// elements with bounding boxes, as well as page geometry, and page and line languages.
        /// </summary>
        public OcrExtractedPage[] ExtractedPages { get; internal set; }
    }
}