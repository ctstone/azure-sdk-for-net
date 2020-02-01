// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Arguments
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

        internal static void IfNullOrEmpty(string arg, string name)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentException("Argument must not be null or empty.", name);
            }
        }

        internal static void IfInvalidUri(Uri uri, string name)
        {
            Throw.IfMissing(uri, name);

            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentException("Uri must be absolute.", name);
            }

            if (uri.Scheme != "http" && uri.Scheme != "https")
            {
                throw new ArgumentException("Uri scheme must be http or https.", name);
            }

            if (!uri.IsWellFormedOriginalString() || !uri.IsAbsoluteUri)
            {
                throw new ArgumentException("Uri must be well formed.", name);
            }
        }
    }
}