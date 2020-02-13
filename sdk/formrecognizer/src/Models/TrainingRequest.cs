// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Request parameter to train a new custom model.
    /// </summary>
    internal class TrainingRequest
    {
        /// <summary>
        /// Source path containing the training documents.
        ///
        /// Typically this is a Uri to an __Azure Storage blob container__.
        ///
        /// _When hosting the Form Recognizer service __on-premises__ in a local container, this should be a Unix-style path to the directory containing the training documents._
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Filter to apply to the documents in the source path for training.
        /// </summary>
        public SourceFilter Filter { get; set; }

        /// <summary>
        /// Use label file for training a model.
        ///
        /// A label file is a set of human-generated annotations that is stored at the same location as your training documents.
        ///
        /// See the [sample labeling tool](https://docs.microsoft.com/en-us/azure/cognitive-services/form-recognizer/quickstarts/label-tool) for more information.
        /// </summary>
        public bool? UseLabelFile { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingRequest"/> class.
        /// </summary>
        /// <param name="source">
        /// Source path containing the training documents.
        ///
        /// Typically this is a Uri to an __Azure Storage blob container__.
        ///
        /// _When hosting the Form Recognizer service __on-premises__ in a local container, this should be a Unix-style path to the directory containing the training documents._
        /// </param>
        /// <param name="filter">Source filter.</param>
        /// <param name="useLabelFile">
        /// Use label file for training a model.
        ///
        /// A label file is a set of human-generated annotations that is stored at the same location as your training documents.
        ///
        /// See the [sample labeling tool](https://docs.microsoft.com/en-us/azure/cognitive-services/form-recognizer/quickstarts/label-tool) for more information.
        /// </param>
        public TrainingRequest(string source, SourceFilter filter = null, bool? useLabelFile = null)
        {
            Source = source;
            Filter = filter;
            UseLabelFile = useLabelFile;
        }
    }
}