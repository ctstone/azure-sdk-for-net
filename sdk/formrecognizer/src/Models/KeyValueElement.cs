// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted key or value in a key-value pair.
    /// </summary>
    public class KeyValueElement : ReferencingTextElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueElement"/> class.
        /// </summary>
        protected KeyValueElement()
        { }

        internal static KeyValueElement Create() => new KeyValueElement();
    }
}