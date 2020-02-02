// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// Page information extracted by an unsupervised model.
    /// </summary>
    public class ExtractedPage
    {
        /// <summary>
        /// Page number.
        /// </summary>
        public int PageNumber { get; internal set; }

        /// <summary>
        /// Cluster identifier.
        /// </summary>
        public int FormClusterId { get; internal set; }

        /// <summary>
        /// List of key-value pairs extracted from the page.
        /// </summary>
        public ExtractedField[] PageFields { get; internal set; }
    }
}