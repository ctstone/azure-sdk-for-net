// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;

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
            MemoryStream stream;
            if (response.ContentStream is MemoryStream)
            {
                stream = response.ContentStream as MemoryStream;
            }
            else
            {
                stream = new MemoryStream();
                response.ContentStream.CopyTo(stream);
                stream.Position = 0;
            }

            var json = options.Encoding.GetString(stream.ToArray());
            return JsonSerializer.Deserialize<T>(json, options.SerializationOptions);
        }

        public static void ExpectStatus(this Response response, HttpStatusCode statusCode, FormRecognizerClientOptions options)
        {
            if (response.Status != (int)statusCode)
            {
                var isJson = response.Headers.ContentType != default && response.Headers.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase);
                if (isJson)
                {
                    var error = response.GetJsonContent<ErrorResponse>(options);
                    var message = error.Error.Message ?? "Request failed";
                    var code = error.Error.Code ?? "GeneralError";
                    throw new RequestFailedException(response.Status, $"{response.Status} ({code}) - {message}", code, null);
                }
                else
                {
                    var message = "Request failed";
                    var code = "GeneralError";
                    throw new RequestFailedException(response.Status, $"{response.Status} ({code}) - {message}", code, null);
                }
            }
        }
    }
}