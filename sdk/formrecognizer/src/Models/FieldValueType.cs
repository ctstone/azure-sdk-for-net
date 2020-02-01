// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Semantic data type of the field value.
    /// </summary>
    internal enum FieldValueType
    {
        /// <summary>String.</summary>
        StringType = 1,

        /// <summary>Date.</summary>
        Date,

        /// <summary>Time.</summary>
        Time,

        /// <summary>PhoneNumber.</summary>
        PhoneNumber,

        /// <summary>Number.</summary>
        Number,

        /// <summary>Integer.</summary>
        IntegerType,

        /// <summary>Array.</summary>
        Array,

        /// <summary>Object.</summary>
        ObjectType,
    }
}