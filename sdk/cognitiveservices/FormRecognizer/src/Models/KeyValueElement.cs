// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted key or value in a key-value pair.
    /// </summary>
    public class KeyValueElement : TextElement
    {
        internal string[] ElementReferences { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueElement"/> class.
        /// </summary>
        protected KeyValueElement()
        { }

        /// <summary>
        /// Resolve the text elements referenced in the `ElementReferences` field.
        /// </summary>
        /// <param name="root">The root analysis result object.</param>
        public TextElement[] GetElements(AnalysisResult root)
        {
            Console.WriteLine($"{root}-{this.ElementReferences.Length}");
            throw new NotImplementedException();
        }

        internal static KeyValueElement Create() => new KeyValueElement();
    }
}