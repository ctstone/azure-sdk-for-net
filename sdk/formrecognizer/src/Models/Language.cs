// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Language information.
    /// </summary>
    public readonly struct Language : IEquatable<Language>
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> struct.
        /// </summary>
        /// <param name="value">Language.</param>
        internal Language(string value)
        {
            _value = value;
        }

        /// <summary>
        ///  English.
        /// </summary>
        public static Language English => "en";

        /// <summary>
        /// Spanish.
        /// </summary>
        public static Language Spanish => "es";

        /// <inheritdoc />
        public bool Equals(Language other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is Language other && Equals(other);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => _value;

        /// <summary>
        /// Convert a string to a <see cref="Language" />.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator Language(string value) => new Language(value);

        /// <summary>
        /// Determines if two <see cref="Language"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="Language"/> to compare.</param>
        /// <param name="right">The first <see cref="Language"/> to compare.</param>
        public static bool operator ==(Language left, Language right) => left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="Language"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="Language"/> to compare.</param>
        /// <param name="right">The first <see cref="Language"/> to compare.</param>
        public static bool operator !=(Language left, Language right) => !left.Equals(right);
    }
}