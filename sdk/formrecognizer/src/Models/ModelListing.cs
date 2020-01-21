// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Model listing.
    /// </summary>
    public class ModelListing
    {
        /// <summary>
        /// Summary of models.
        /// </summary>
        public ModelsSummary Summary { get; internal set; }

        /// <summary>
        /// Page of models.
        /// </summary>
        public ModelInfo[] ModelList { get; internal set; }

        /// <summary>
        /// Link to next page of models.
        /// </summary>
        public string NextLink { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelListing"/> struct.
        /// </summary>
        protected ModelListing()
        { }

        internal static ModelListing Create() => new ModelListing();
    }
}