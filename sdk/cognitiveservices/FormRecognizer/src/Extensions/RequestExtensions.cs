// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
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

        public static void AddFormContent(this Request request, FormContentType? contentType, Stream stream)
        {
            stream.ThrowIfCannotRead("Stream to analyze is not readable.", nameof(stream));
            if (contentType == default)
            {
                stream.ThrowIfCannotSeek("Content-Type must be provided when stream is not seekable.", nameof(contentType));
                if (!stream.TryGetContentType(out contentType))
                {
                    throw new ArgumentNullException("Cannot get Content-Type of stream. Try providing a Content-Type parameter.", nameof(contentType));
                }
            }
            request.Headers.Add(FormHttpHeader.Common.ForContentType(contentType.Value));
            request.Content = RequestContent.Create(stream);

        }
    }
}