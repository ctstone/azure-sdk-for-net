// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Model Info
    /// </summary>
    public struct ModelInfo
    {
        /// <summary>
        /// Model Id.
        /// </summary>
        public string ModelId { get; set; }

        /// <summary>
        /// Model status.
        /// </summary>
        public ModelStatus Status { get; set; }
    }
}