// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Models summary
    /// </summary>
    public struct ModelsSummary
    {
        /// <summary>
        /// Count.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Limit.
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Last updated date.
        /// </summary>
        public DateTimeOffset LastUpdatedDateTime { get; set; }
    }
}