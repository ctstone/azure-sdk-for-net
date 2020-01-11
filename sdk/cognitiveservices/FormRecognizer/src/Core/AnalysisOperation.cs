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
    /// Analysis operation.
    /// </summary>
    public class AnalysisOperation : Operation<AnalyzedForm>
    {
        private const string LocationHeader = "Operation-Location";
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(10);
        private readonly string _modelId;
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;
        private AnalyzedForm? _value;
        private Response _response;

        /// <inheritdoc/>
        public override string Id => _id;

        /// <inheritdoc/>
        public override AnalyzedForm Value => HasValue ? _value.Value : default;

        /// <inheritdoc/>
        public override bool HasCompleted => _value?.IsAnalysisComplete() ?? false;

        /// <inheritdoc/>
        public override bool HasValue => _value?.IsAnalysisSuccess() ?? false;

        internal AnalysisOperation(HttpPipeline pipeline, string modelId, string id, FormRecognizerClientOptions options)
        {
            _modelId = modelId;
            _id = id;
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Analysis operation.
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
        public override ValueTask<Response<AnalyzedForm>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return WaitForCompletionAsync(DefaultPollingInterval, cancellationToken);
        }

        /// <inheritdoc/>
        public async override ValueTask<Response<AnalyzedForm>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default)
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
            return Response.FromValue(_value.Value, _response);
        }

        private Response UpdateStatus(Response response)
        {
            _response = response;
            response.ExpectStatus(HttpStatusCode.OK, _options);
            var analysis = response.GetJsonContent<AnalyzedForm>(_options);
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