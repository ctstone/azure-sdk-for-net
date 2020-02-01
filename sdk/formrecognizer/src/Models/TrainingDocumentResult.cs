// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Report for a custom model training document.
    /// </summary>
    internal class TrainingDocumentResult
    {
        /// <summary>
        /// Training document name.
        /// </summary>
        public string DocumentName { get; internal set; }

        /// <summary>
        /// Total number of pages trained.
        /// </summary>
        public int Pages { get; internal set; }


        // TODO: Are these errors about the document, or the training operation, or ... ?
        // What are the possible values of these?  Would it be better to group these with
        // training errors?  How will customers use these errors?

        /// <summary>
        /// List of errors.
        /// </summary>
        public FormRecognizerError[] Errors { get; internal set; }

        // TODO: What does it mean for a training document to have failed or succeeded?
        // Is this in the context of either supervised or unsupervised models, or does it apply to both?
        // What does it mean to have partially succeeded?  What will customers do with this information?

        /// <summary>
        /// Status of the training operation.
        /// </summary>
        public TrainingStatus Status { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDocumentResult"/> class.
        /// </summary>
        protected TrainingDocumentResult()
        { }

        internal static TrainingDocumentResult Create() => new TrainingDocumentResult();
    }
}