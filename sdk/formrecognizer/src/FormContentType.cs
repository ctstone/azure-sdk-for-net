// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// Form content type for local files.
    /// </summary>
    public readonly struct FormContentType : IEquatable<FormContentType>
    {
        internal const string DefaultContentType = "application/octet-stream";
        private readonly string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormContentType"/> struct.
        /// </summary>
        /// <param name="value">MIME type of form document.</param>
        public FormContentType(string value)
        {
            _value = value ?? DefaultContentType;
        }

        /// <summary>
        /// Pdf.
        /// </summary>
        public static FormContentType Pdf => "application/pdf";

        /// <summary>
        /// Png.
        /// </summary>
        public static FormContentType Png => "image/png";

        /// <summary>
        /// Jpeg.
        /// </summary>
        public static FormContentType Jpeg => "image/jpeg";

        /// <summary>
        /// Tiff.
        /// </summary>
        public static FormContentType Tiff => "image/tiff";

        /// <inheritdoc />
        public bool Equals(FormContentType other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is FormContentType other && Equals(other);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => _value;

        /// <summary>
        /// Converts a string to a <see cref="FormContentType"/>.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator FormContentType(string value) => new FormContentType(value);

        /// <summary>
        /// Determines if two <see cref="FormContentType"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="FormContentType"/> to compare.</param>
        /// <param name="right">The first <see cref="FormContentType"/> to compare.</param>
        public static bool operator ==(FormContentType left, FormContentType right) => left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="FormContentType"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="FormContentType"/> to compare.</param>
        /// <param name="right">The first <see cref="FormContentType"/> to compare.</param>
        public static bool operator !=(FormContentType left, FormContentType right) => !left.Equals(right);
    }
}