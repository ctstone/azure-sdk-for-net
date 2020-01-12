// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Represents an extracted word.
    /// </summary>
    public class TextWord : PredictedTextElement<float>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextWord"/> class.
        /// </summary>
        protected TextWord()
        { }

        internal static TextWord Create() => new TextWord();
    }
}