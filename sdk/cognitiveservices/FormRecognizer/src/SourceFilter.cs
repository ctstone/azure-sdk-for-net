// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Source Filter.
    /// </summary>
    public struct SourceFilter
    {
        /// <summary>
        /// Prefix.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// IncludeSubFolders
        /// </summary>
        public bool IncludeSubFolders { get; set; }
    }
}