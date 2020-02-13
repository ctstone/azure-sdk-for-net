// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Training request source filter.
    /// </summary>
    public class SourceFilter
    {
        /// <summary>
        /// A case-sensitive prefix string to filter documents in the source path for training. For example, when using an Azure storage container URI, use the prefix to restrict sub folders for training.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// A flag to indicate if subfolders within the set of prefix folders should also be included when scanning the source for training content.
        /// </summary>
        public bool? IncludeSubFolders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SourceFilter"/> class.
        /// </summary>
        /// <param name="prefix">A case-sensitive prefix string to filter documents in the source path for training. For example, when using an Azure storage container URI, use the prefix to restrict sub folders for training.</param>
        /// <param name="includeSubFolders">A flag to indicate if subfolders within the set of prefix folders should also be included when scanning the source for training content.</param>
        public SourceFilter(string prefix = default, bool? includeSubFolders = default)
        {
            Prefix = prefix;
            IncludeSubFolders = includeSubFolders;
        }
    }
}