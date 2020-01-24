// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Represents a line of extracted text.
    /// </summary>
    public class TextLine : TextElement
    {

        /// <summary>
        /// The detected language of the line, if different from the overall page language.
        /// </summary>
        /// <value></value>
        public Language Language { get; internal set; }

        /// <summary>
        /// List of words extracted from the line.
        /// </summary>
        public IList<TextWord> Words { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextLine"/> class.
        /// </summary>
        protected TextLine()
        { }

        internal static TextLine Create() => new TextLine();
    }
}