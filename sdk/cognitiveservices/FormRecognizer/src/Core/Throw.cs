// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Core
{
    internal static class Throw
    {
        public static void IfMissing<T>(T arg, string name)
        {
            if (EqualityComparer<T>.Default.Equals(arg, default(T)))
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

        public static void IfUriNotWellFormed(Uri uri, string name)
        {
            if (!uri.IsWellFormedOriginalString())
            {
                throw new ArgumentException("Uri must be well formed.", name);
            }
        }
    }
}