// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted key or value in a key-value pair.
    /// </summary>
    public class KeyValueElement : TextElement
    {
        /// <summary>
        /// When includeTextDetails is set to true, a list of references to the text elements constituting this key or value.
        /// </summary>
        public TextElement[] Elements { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueElement"/> class.
        /// </summary>
        protected KeyValueElement()
        { }

        internal static KeyValueElement Create() => new KeyValueElement();
    }
}