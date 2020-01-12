// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Base class for extracted text elements with confidence value.
    /// </summary>
    public abstract class PredictedTextElement<T> : TextElement
    {
        /// <summary>
        /// Confidence value.
        /// </summary>
        public T Confidence { get; internal set; }
    }
}