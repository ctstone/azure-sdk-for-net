// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Semantic data type of a <see cref="PredefinedField" />
    /// </summary>
    public struct PredefinedFieldType : IEquatable<PredefinedFieldType>
    {
        private readonly string _value;

        /// <summary>
        /// String.
        /// </summary>
        public static PredefinedFieldType StringType => "string";

        /// <summary>
        /// Date.
        /// </summary>
        public static PredefinedFieldType Date => "date";

        /// <summary>
        /// Time.
        /// </summary>
        public static PredefinedFieldType Time => "time";

        /// <summary>
        /// Phone number.
        /// </summary>
        public static PredefinedFieldType PhoneNumber => "phoneNumber";

        /// <summary>
        /// Number.
        /// </summary>
        public static PredefinedFieldType Number => "number";

        /// <summary>
        /// Integer.
        /// </summary>
        public static PredefinedFieldType IntegerType => "integer";

        /// <summary>
        /// Array.
        /// </summary>
        public static PredefinedFieldType Array => "array";

        /// <summary>
        /// Object.
        /// </summary>
        public static PredefinedFieldType ObjectType => "object";

        internal PredefinedFieldType(string value) => _value = value;

        /// <inheritdoc />
        public bool Equals(PredefinedFieldType other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is PredefinedFieldType other && Equals(other);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => _value;

        /// <summary>
        /// Convert a string to a <see cref="PredefinedFieldType" />.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        public static implicit operator PredefinedFieldType(string value) => new PredefinedFieldType(value);

        /// <summary>
        /// Determines if two <see cref="PredefinedFieldType"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="PredefinedFieldType"/> to compare.</param>
        /// <param name="right">The first <see cref="PredefinedFieldType"/> to compare.</param>
        public static bool operator ==(PredefinedFieldType left, PredefinedFieldType right) => left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="PredefinedFieldType"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="PredefinedFieldType"/> to compare.</param>
        /// <param name="right">The first <see cref="PredefinedFieldType"/> to compare.</param>
        public static bool operator !=(PredefinedFieldType left, PredefinedFieldType right) => !left.Equals(right);
    }
}