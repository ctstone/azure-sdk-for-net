// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Keys extracted by the custom model.
    /// </summary>
    public struct KeysResult
    {
        /// <summary>
        /// Object mapping clusterIds to a list of keys.
        /// </summary>
        public IDictionary<string, string[]> Clusters { get; internal set; }

        internal static KeysResult Create() => new KeysResult();
    }
}