// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Information about the extracted key-value pair.
    /// </summary>
    public class KeyValuePair
    {
        /// <summary>
        /// A user defined label for the key/value pair entry.
        /// </summary>
        public string Label { get; internal set; }

        /// <summary>
        /// Information about the extracted key in a key-value pair.
        /// </summary>
        public KeyValueElement Key { get; internal set; }

        /// <summary>
        /// Information about the extracted value in a key-value pair.
        /// </summary>
        public KeyValueElement Value { get; internal set; }

        /// <summary>
        /// Confidence value.
        /// </summary>
        public float Confidence { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValuePair"/> class.
        /// </summary>
        protected KeyValuePair()
        { }

        internal static KeyValuePair Create() => new KeyValuePair();
    }
}