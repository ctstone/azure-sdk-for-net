// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Operations
{
    /// <summary>
    /// Analysis operation.
    /// </summary>
    public class AnalysisOperation : Operation<AnalyzedForm>
    {
        private const string LocationHeader = "Operation-Location";
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;

        /// <inheritdoc/>
        public override string Id => throw new NotImplementedException();

        /// <inheritdoc/>
        public override AnalyzedForm Value => throw new NotImplementedException();

        /// <inheritdoc/>
        public override bool HasCompleted => throw new NotImplementedException();

        /// <inheritdoc/>
        public override bool HasValue => throw new NotImplementedException();

        internal AnalysisOperation(HttpPipeline pipeline, string id, FormRecognizerClientOptions options)
        {
            _id = id;
            _pipeline = pipeline;
            _options = options;
        }

        /// <inheritdoc/>
        public override Response GetRawResponse()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override ValueTask<Response<AnalyzedForm>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override ValueTask<Response<AnalyzedForm>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        internal static string GetAnalysisOperationId(Response response)
        {
            string location;
            if (!response.Headers.TryGetValue(LocationHeader, out location))
            {
                throw new RequestFailedException("Unable to retrieve analysis location URL.");
            }

            var i = location.LastIndexOf('/');
            if (i == -1)
            {
                throw new RequestFailedException("Unable to parse analysis location URL.");
            }

            return location.Substring(i + 1);
        }
    }
}