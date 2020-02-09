// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Report for a custom model training document.
    /// </summary>
    public class TrainingDocument
    {
        /// <summary>
        /// Training document name.
        /// </summary>
        public string DocumentName { get; internal set; }

        /// <summary>
        /// Total number of pages trained.
        /// </summary>
        public int PageCount { get; internal set; }

        /// <summary>
        /// List of errors.
        /// </summary>
        public ErrorDetails[] Errors { get; internal set; }

        /// <summary>
        /// Status of the training operation.
        /// </summary>
        public TrainingStatus Status { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingDocument"/> class.
        /// </summary>
        protected TrainingDocument()
        { }

        internal static TrainingDocument Create() => new TrainingDocument();

        /// <inheritdoc />
        public override string ToString()
        {
            return $"TrainingDocument(name={DocumentName}, status={Status}, pages={PageCount}, error={Errors.Any()})";
        }
    }
}