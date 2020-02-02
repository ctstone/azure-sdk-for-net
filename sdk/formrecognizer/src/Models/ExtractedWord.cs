﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// Represents an extracted word.
    /// </summary>
    public class ExtractedWord
    {
        /// <summary>
        /// Confidence value in the prediction of the word.
        /// </summary>
        public float Confidence { get; internal set; }

        /// <summary>
        /// The text content of the line.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Bounding box of the extracted line.
        /// </summary>
        public float[] BoundingBox { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractedWord"/> class.
        /// </summary>
        protected ExtractedWord()
        { }

        internal static ExtractedWord Create() => new ExtractedWord();
    }
}