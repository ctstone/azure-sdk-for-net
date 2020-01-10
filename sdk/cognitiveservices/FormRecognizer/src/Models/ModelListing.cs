// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Model listing.
    /// </summary>
    public struct ModelListing
    {
        /// <summary>
        /// Summary of models.
        /// </summary>
        public ModelsSummary Summary { get; set; }

        /// <summary>
        /// Page of models.
        /// </summary>
        public IList<ModelInfo> ModelList { get; set; }

        /// <summary>
        /// Link to next page of models.
        /// </summary>
        public string NextLink { get; set; }
    }
}