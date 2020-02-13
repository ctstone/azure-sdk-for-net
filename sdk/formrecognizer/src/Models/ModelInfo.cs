// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Model Info.
    /// </summary>
    public class ModelInfo
    {
        /// <summary>
        /// Model Id.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Model status.
        /// </summary>
        public ModelStatus Status { get; internal set; }

        /// <summary>
        /// Date and time when the model was created.
        /// </summary>
        public DateTimeOffset CreatedOn { get; internal set; }

        /// <summary>
        /// Date and time when the status was last updated.
        /// </summary>
        public DateTimeOffset LastUpdatedOn { get; internal set; }

        /// <summary>
        /// Get the time taken to train the model.
        /// </summary>
        public TimeSpan TrainingDuration => LastUpdatedOn - CreatedOn;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelInfo"/> class.
        /// </summary>
        protected ModelInfo() { }

        internal static ModelInfo Create() => new ModelInfo();

        /// <inheritdoc />
        public override string ToString()
        {
            return $"CustomFormModel(id={Id}, status={Status}, created={CreatedOn})";
        }
    }
}