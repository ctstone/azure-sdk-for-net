// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class ExtractedFieldName
    {
        /// <summary>
        /// The text content of the line.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Bounding box of the extracted line.
        /// </summary>
        public float[] BoundingBox { get; internal set; }
    }
}
