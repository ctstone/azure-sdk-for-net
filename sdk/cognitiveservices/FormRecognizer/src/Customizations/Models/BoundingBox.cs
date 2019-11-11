// Copyright (c) Microsoft Corporation. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class BoundingBox : List<float>
    {
        public BoundingBox(IEnumerable<float> points)
        {
            this.AddRange(points);
        }

        public float TopLeftX { get => this[0]; }
        public float TopLeftY { get => this[1]; }
        public float TopRightX { get => this[2]; }
        public float TopRightY { get => this[3]; }
        public float BottomRightX { get => this[4]; }
        public float BottomRightY { get => this[5]; }
        public float BottomLeftX { get => this[6]; }
        public float BottomLeftY { get => this[7]; }
        public float MinY { get => new List<float> { this[1], this[3], this[5], this[7] }.Min(); }
        public float MaxY { get => new List<float> { this[1], this[3], this[5], this[7] }.Max(); }
        public float MinX { get => new List<float> { this[0], this[2], this[4], this[6] }.Min(); }
        public float MaxX { get => new List<float> { this[0], this[2], this[4], this[6] }.Max(); }

        public float ComputeAngle()
        {
            // Compute the angle of the line from the left center to the right center points.
            // Mathematically optimize out the averaging in the computation as it does not affect end result.
            var leftCenterX = this[0] + this[6];
            var leftCenterY = this[1] + this[7];
            var rightCenterX = this[2] + this[4];
            var rightCenterY = this[3] + this[5];
            return (float)Math.Atan2(rightCenterY - leftCenterY, rightCenterX - leftCenterX);
        }

        public BoundingBox Rotate(float angle)
        {
            var result = new float[8];
            (result[0], result[1]) = Rotate(this[0], this[1], angle);
            (result[2], result[3]) = Rotate(this[2], this[3], angle);
            (result[4], result[5]) = Rotate(this[4], this[5], angle);
            (result[6], result[7]) = Rotate(this[6], this[7], angle);
            return new BoundingBox(result);
        }

        static private (float rotatedX, float rotatedY) Rotate(float x, float y, double angle)
        {
            var rotatedX = x * Math.Cos(angle) - y * Math.Sin(angle);
            var rotatedY = x * Math.Sin(angle) + y * Math.Cos(angle);
            return ((float)rotatedX, (float)rotatedY);
        }
    }
}
