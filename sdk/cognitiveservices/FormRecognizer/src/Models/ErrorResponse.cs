// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Error response.
    /// </summary>
    public struct ErrorResponse
    {
        /// <summary>
        /// Error details.
        /// </summary>
        public ErrorDetails Error { get; set; }
    }
}