// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// Represents a long-running analysis operation.
    /// </summary>
    public class AnalyzeOperation<TAnalysis> : Operation<TAnalysis>
        where TAnalysis : class
    {
        private const int DefaultPollingIntervalSeconds = 10;
        private const string LocationHeader = "Operation-Location";
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(DefaultPollingIntervalSeconds);
        private readonly string _basePath;
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly JsonSerializerOptions _options;
        private TAnalysis _analysis;
        private AnalysisInternal _value;
        private Response _response;
        private readonly Func<AnalysisInternal, TAnalysis> _analysisFactory;

        /// <summary>
        /// Get the ID of the analysis operation. This value can be used to poll for the status of the analysis outcome.
        /// </summary>
        public override string Id => _id;

        /// <summary>
        /// The final result of the analysis operation, if the operation completed successfully.
        /// </summary>
        public override TAnalysis Value => HasValue ? _analysis : default;

        /// <summary>
        /// True if the analysis operation completed.
        /// </summary>
        public override bool HasCompleted => _value?.IsAnalysisComplete() ?? false;

        /// <summary>
        /// True if the analysis operation completed successfully.
        /// </summary>
        public override bool HasValue => _value?.IsAnalysisSuccess() ?? false;

        internal AnalyzeOperation(HttpPipeline pipeline, string basePath, string id, JsonSerializerOptions options, Func<AnalysisInternal, TAnalysis> analysisFactory)
        {
            _basePath = basePath;
            _id = id;
            _pipeline = pipeline;
            _options = options;
            _analysisFactory = analysisFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalyzeOperation{TAnalysis}"/> class for mocking.
        /// </summary>
        protected AnalyzeOperation()
        { }

        /// <inheritdoc/>
        public override Response GetRawResponse()
        {
            return _response;
        }

        /// <inheritdoc/>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(_basePath, _id))
            {
                return UpdateStatus(_pipeline.SendRequest(request, cancellationToken));
            }
        }

        /// <inheritdoc/>
        public async override ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetAnalysisRequest(_basePath, _id))
            {
                return UpdateStatus(await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false));
            }
        }

        /// <inheritdoc/>
        public override ValueTask<Response<TAnalysis>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return WaitForCompletionAsync(DefaultPollingInterval, cancellationToken);
        }

        /// <inheritdoc/>
        public async override ValueTask<Response<TAnalysis>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default)
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
            return Response.FromValue(_analysis, _response);
        }

        private Response UpdateStatus(Response response)
        {
            _response = response;
            response.ExpectStatus(HttpStatusCode.OK, _options);
            var analysis = response.GetJsonContent<AnalysisInternal>(_options);
            if (analysis.IsAnalysisComplete())
            {
                _value = analysis;
                _analysis = _analysisFactory(_value);
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

        internal static AnalyzeOperation<TAnalysis> FromResponse(HttpPipeline pipeline, string basePath, Response response, JsonSerializerOptions options, Func<AnalysisInternal, TAnalysis> analysisFactory)
        {
            response.ExpectStatus(HttpStatusCode.Accepted, options);
            var id = GetAnalysisOperationId(response);
            return new AnalyzeOperation<TAnalysis>(pipeline, basePath, id, options, analysisFactory);
        }
    }
}