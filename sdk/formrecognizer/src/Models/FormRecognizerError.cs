// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Error response.
    /// </summary>
    public struct FormRecognizerError
    {
        // TODO: Make this look as much as possible as TextAnalyticsError.

        /// <summary>
        /// Error status.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error code.
        /// </summary>
        public string Code { get; set; }

        internal FormRecognizerError(string message, string code)
        {
            Message = message;
            Code = code;
        }
    }
}