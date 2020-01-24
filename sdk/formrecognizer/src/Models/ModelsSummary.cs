// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Models summary
    /// </summary>
    public class ModelsSummary
    {
        /// <summary>
        /// Count.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Limit.
        /// </summary>
        public int Limit { get; internal set; }

        /// <summary>
        /// Last updated date.
        /// </summary>
        public DateTimeOffset LastUpdatedOn { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsSummary"/> class.
        /// </summary>
        protected ModelsSummary()
        { }

        internal static ModelsSummary Create() => new ModelsSummary();
    }
}