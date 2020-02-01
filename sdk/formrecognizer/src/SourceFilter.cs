// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Training request source filter.
    /// </summary>
    internal class SourceFilter
    {
        /// <summary>
        /// A case-sensitive prefix string to filter documents in the source path for training. For example, when using an Azure storage container URI, use the prefix to restrict sub folders for training.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// A flag to indicate if subfolders within the set of prefix folders will also need to be included when searching for content to be preprocessed.
        /// </summary>
        public bool? IncludeSubFolders { get; set; }
    }
}