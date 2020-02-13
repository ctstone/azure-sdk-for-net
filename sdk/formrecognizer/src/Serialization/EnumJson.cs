// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class EnumJson
    {
        public static T Read<T>(JsonElement root)
            where T : Enum
        {
            return (T)Enum.Parse(typeof(T), root.GetString(), true);
        }
    }
}