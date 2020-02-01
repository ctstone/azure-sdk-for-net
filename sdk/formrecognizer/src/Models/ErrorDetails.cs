// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Error response.
    /// </summary>
    internal struct ErrorDetails
    {
        /// <summary>
        /// Error status.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error code.
        /// </summary>
        public string Code { get; set; }

        internal ErrorDetails(string message, string code)
        {
            Message = message;
            Code = code;
        }
    }
}