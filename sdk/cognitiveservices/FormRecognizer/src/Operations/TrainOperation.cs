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
    /// Train operation.
    /// </summary>
    public class TrainOperation : Operation<FormModel>
    {
        private const string LocationHeader = "Location";
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(10);
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;
        private FormModel? _value;
        private Response _response;

        /// <inheritdoc/>
        public override string Id => _id;

        /// <inheritdoc/>
        public override FormModel Value => HasValue ? _value.Value : default;

        /// <inheritdoc/>
        public override bool HasCompleted => _value?.IsModelComplete() ?? false;

        /// <inheritdoc/>
        public override bool HasValue => _value?.IsModelSuccess() ?? false;

        internal TrainOperation(HttpPipeline pipeline, string id, FormRecognizerClientOptions options)
        {
            _id = id;
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Train operation.
        /// </summary>
        protected TrainOperation()
        { }

        /// <inheritdoc/>
        public override Response GetRawResponse()
        {
            return _response;
        }

        /// <inheritdoc/>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(Id))
            {
                return UpdateStatus(_pipeline.SendRequest(request, cancellationToken));
            }
        }

        /// <inheritdoc/>
        public override async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(Id))
            {
                return UpdateStatus(await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false));
            }
        }

        /// <inheritdoc/>
        public override ValueTask<Response<FormModel>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return WaitForCompletionAsync(DefaultPollingInterval, cancellationToken);
        }

        /// <inheritdoc/>
        public async override ValueTask<Response<FormModel>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
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
            var model = response.GetJsonContent<FormModel>(_options);
            if (model.IsModelComplete())
            {
                _value = model;
            }
            return response;
        }

        internal static string GetTrainingOperationId(Response response)
        {
            string location;
            if (!response.Headers.TryGetValue(LocationHeader, out location))
            {
                throw new RequestFailedException("Unable to retrieve train location URL.");
            }

            var i = location.LastIndexOf('/');
            if (i == -1)
            {
                throw new RequestFailedException("Unable to parse train location URL.");
            }

            return location.Substring(i + 1);
        }
    }
}