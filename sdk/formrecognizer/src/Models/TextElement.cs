// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Base class for extracted text elements.
    /// </summary>
    public abstract class TextElement
    {
        /// <summary>
        /// The text content of the line.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Bounding box of the extracted line.
        /// </summary>
        public float[] BoundingBox { get; internal set; }
    }
}