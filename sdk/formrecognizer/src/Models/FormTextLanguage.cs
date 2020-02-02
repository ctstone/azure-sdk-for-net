// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Azure.AI.FormRecognizer.Prediction
{
    // TODO: Normalize language across Cognitive

    /// <summary>
    /// Language information.
    /// </summary>
    public readonly struct FormTextLanguage : IEquatable<FormTextLanguage>
    {
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormTextLanguage"/> struct.
        /// </summary>
        /// <param name="value">Language.</param>
        internal FormTextLanguage(string value)
        {
            _value = value;
        }

        /// <summary>
        ///  English.
        /// </summary>
        public static FormTextLanguage English => "en";

        /// <summary>
        /// Spanish.
        /// </summary>
        public static FormTextLanguage Spanish => "es";

        /// <inheritdoc />
        public bool Equals(FormTextLanguage other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is FormTextLanguage other && Equals(other);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => _value;

        /// <summary>
        /// Convert a string to a <see cref="FormTextLanguage" />.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator FormTextLanguage(string value) => new FormTextLanguage(value);

        /// <summary>
        /// Determines if two <see cref="FormTextLanguage"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="FormTextLanguage"/> to compare.</param>
        /// <param name="right">The first <see cref="FormTextLanguage"/> to compare.</param>
        public static bool operator ==(FormTextLanguage left, FormTextLanguage right) => left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="FormTextLanguage"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="FormTextLanguage"/> to compare.</param>
        /// <param name="right">The first <see cref="FormTextLanguage"/> to compare.</param>
        public static bool operator !=(FormTextLanguage left, FormTextLanguage right) => !left.Equals(right);
    }
}