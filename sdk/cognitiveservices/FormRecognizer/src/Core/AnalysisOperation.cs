// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Core
{
    /// <summary>
    /// Represents a long-running analysis operation.
    /// </summary>
    public class AnalysisOperation : Operation<Analysis>
    {
        private const string LocationHeader = "Operation-Location";
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(10);
        private readonly string _modelId;
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;
        private Analysis _value;
        private Response _response;

        /// <summary>
        /// Get the ID of the analysis operation. This value can be used to poll for the status of the analysis outcome.
        /// </summary>
        public override string Id => _id;

        /// <summary>
        /// The final result of the analysis operation, if the operation completed successfully.
        /// </summary>
        public override Analysis Value => HasValue ? _value : default;

        /// <summary>
        /// True if the analysis operation completed.
        /// </summary>
        public override bool HasCompleted => _value?.IsAnalysisComplete() ?? false;

        /// <summary>
        /// True if the analysis operation completed successfully.
        /// </summary>
        public override bool HasValue => _value?.IsAnalysisSuccess() ?? false;

        internal AnalysisOperation(HttpPipeline pipeline, string modelId, string id, FormRecognizerClientOptions options)
        {
            _modelId = modelId;
            _id = id;
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisOperation"/> class for mocking.
        /// </summary>
        protected AnalysisOperation()
        { }

        /// <inheritdoc/>
        public override Response GetRawResponse()
        {
            return _response;
        }

        /// <inheritdoc/>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(_modelId, Id))
            {
                return UpdateStatus(_pipeline.SendRequest(request, cancellationToken));
            }
        }

        /// <inheritdoc/>
        public async override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(_modelId, Id))
            {
                return UpdateStatus(await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false));
            }
        }

        /// <inheritdoc/>
        public override ValueTask<Response<Analysis>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return WaitForCompletionAsync(DefaultPollingInterval, cancellationToken);
        }

        /// <inheritdoc/>
        public async override ValueTask<Response<Analysis>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default)
        {
            do
            {
                await UpdateStatusAsync(cancellationToken).ConfigureAwait(false);
                if (!HasCompleted)
                {
                    await Task.Delay(pollingInterval).ConfigureAwait(false);
                }
            }
            while (!HasCompleted);
            return Response.FromValue(_value, _response);
        }

        private Response UpdateStatus(Response response)
        {
            _response = response;
            response.ExpectStatus(HttpStatusCode.OK, _options);
            var analysis = response.GetJsonContent<Analysis>(_options);
            if (analysis.IsAnalysisComplete())
            {
                _value = analysis;
            }
            return response;
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