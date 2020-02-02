// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.AI.FormRecognizer.Prediction
{
    // TODO: Make extensible enum

    /// <summary>
    /// The unit used by the width, height and boundingBox properties.
    /// </summary>
    public enum FormGeometryUnit
    {
        /// <summary>Pixel.</summary>
        Pixel = 1,

        /// <summary>Inch.</summary>
        Inch = 2,
    }
}
