// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    using Models;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text.RegularExpressions;
    using System;

    /// <summary>
    /// Extension methods for FormRecognizerClient.
    /// </summary>
    public static partial class FormRecognizerClientExtensions
    {
        public enum AnalyzeType { Layout, Receipt };

        public static Guid GetGuid(string uri, int order = 1)
        {
            var match = Regex.Match(uri, @"([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})");
            if (match.Success)
            {
                return new Guid(match.Groups[order].ToString());
            }
            throw new ArgumentException("Invalid URL.");
        }

        public static async Task<AnalyzeOperationResult> PollingResultAsync(this IFormRecognizerClient operations, Guid resultid, AnalyzeType type, int retryTimes = 5, CancellationToken cancellationToken = default(CancellationToken))
        {
            int retryTimeframe = 1;
            for (int retryCount = retryTimes; retryCount > 0; retryCount--)
            {
                AnalyzeOperationResult body;
                switch (type)
                {
                    case (AnalyzeType.Layout):
                        body = await GetAnalyzeLayoutResultAsync(operations, resultid, cancellationToken);
                        break;
                    case (AnalyzeType.Receipt):
                        body = await GetAnalyzeReceiptResultAsync(operations, resultid, cancellationToken);
                        break;
                    default:
                        throw new ArgumentException("Not supported analyze type");
                }
                if (body.Status.ToSerializedValue() == "succeeded")
                {
                    return body;
                }
                await Task.Delay(TimeSpan.FromSeconds(retryTimeframe));
                retryTimeframe *= 2;
            }
            throw new ErrorResponseException($"Guid : {resultid.ToString()}, Timeout.");
        }
    }
}
