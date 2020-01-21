// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Status of the training operation.
    /// </summary>
#pragma warning disable CA1717 // plural enum
    public enum TrainingStatus
#pragma warning restore CA1717 // plural enum
    {
        /// <summary>Succeeded.</summary>
        Succeeded = 1,

        /// <summary>Partially succeeded.</summary>
        PartiallySucceeded = 2,

        /// <summary>Failed.</summary>
        Failed = 3,
    }
}