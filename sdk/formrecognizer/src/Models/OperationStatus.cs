// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Model Info
    /// </summary>
#pragma warning disable CA1717 // plural enum
    public enum OperationStatus
#pragma warning restore CA1717 // plural enum
    {
        /// <summary>Not started.</summary>
        NotStarted = 1,

        /// <summary>Running.</summary>
        Running = 2,

        /// <summary>Succeeded.</summary>
        Succeeded = 3,

        /// <summary>Failed.</summary>
        Failed = 4,
    }
}