// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Core
{
    internal static class Throw
    {
        public static void IfMissing<T>(T arg, string name)
        {
            if (arg.Equals(default(T)))
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void IfNullOrEmpty(string arg, string name)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentException("Argument must not be null or empty.", name);
            }
        }
    }
}