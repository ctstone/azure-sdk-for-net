// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Text.Json;
using System.Threading;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Extensions
{
    internal static class RequestExtensions
    {
        public static void AddJsonContent<T>(this Request request, T obj, FormRecognizerClientOptions options)
        {
            var json = JsonSerializer.Serialize(obj, options.SerializationOptions);
            request.Content = RequestContent.Create(options.Encoding.GetBytes(json));
            request.Headers.Add(HttpHeader.Common.JsonContentType);
        }

        public static async void AddJsonContentAsync<T>(this Request request, T obj, FormRecognizerClientOptions options, CancellationToken cancellationToken)
        {
            var memory = new MemoryStream();
            await JsonSerializer.SerializeAsync(memory, obj, options.SerializationOptions, cancellationToken).ConfigureAwait(false);
            memory.Position = 0;
            request.Content = RequestContent.Create(memory);
            request.Headers.Add(HttpHeader.Common.JsonContentType);
        }
    }
}