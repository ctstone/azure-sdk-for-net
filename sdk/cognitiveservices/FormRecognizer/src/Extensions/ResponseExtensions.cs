// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class ResponseExtensions
    {
        public static async Task<T> GetJsonContentAsync<T>(this Response response, FormRecognizerClientOptions options, CancellationToken cancellationToken)
        {
            return await JsonSerializer.DeserializeAsync<T>(response.ContentStream, options.SerializationOptions, cancellationToken);
        }

        public static T GetJsonContent<T>(this Response response, FormRecognizerClientOptions options)
        {
            var memory = new MemoryStream();
            response.ContentStream.CopyTo(memory);
            var json = options.Encoding.GetString(memory.ToArray());
            Console.WriteLine("RESPONSE: " + json);
            return JsonSerializer.Deserialize<T>(json, options.SerializationOptions);
        }
    }
}