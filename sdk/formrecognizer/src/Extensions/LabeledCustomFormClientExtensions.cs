// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Labels
{
    /// <summary>
    /// Extensions to support labeled form operations.
    /// </summary>
    public static class LabeledCustomFormClientExtensions
    {
        /// <summary>
        /// Access a model that uses labels to perform analysis, retrieve metadata, or delete it.
        /// </summary>
        /// <param name="client">Custom form client.</param>
        /// <param name="modelId">Model identifier.</param>
        public static LabeledFormModelReference GetModelReferenceWithLabels(this CustomFormClient client, string modelId) => new LabeledFormModelReference(modelId, client._pipeline, client._options.SerializationOptions);

        /// <summary>
        /// Access a model that uses labels to perform analysis, retrieve metadata, or delete it.
        /// </summary>
        /// <param name="client">Custom form client.</param>
        /// <param name="modelId">Model identifier.</param>
        public static LabeledFormModelReference<TForm> GetModelReferenceWithLabels<TForm>(this CustomFormClient client, string modelId)
            where TForm : new()
        {
            return new LabeledFormModelReference<TForm>(modelId, client._pipeline, client._options.SerializationOptions);
        }
    }
}