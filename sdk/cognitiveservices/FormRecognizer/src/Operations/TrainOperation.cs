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
    public class TrainOperation : Operation<CustomFormModel>
    {
        private const string LocationHeader = "Location";
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(10);
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly FormRecognizerClientOptions _options;
        private CustomFormModel _value;
        private Response _response;

        /// <summary>
        /// Id.
        /// </summary>
        public override string Id => _id;

        /// <summary>
        /// Value.
        /// </summary>
        public override CustomFormModel Value => HasValue ? _value : default;

        /// <summary>
        /// Has completed.
        /// </summary>
        public override bool HasCompleted => _value?.IsModelComplete() ?? false;

        /// <summary>
        /// Has value.
        /// </summary>
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

        /// <summary>
        /// Get raw response.
        /// </summary>
        public override Response GetRawResponse()
        {
            return _response;
        }

        /// <summary>
        /// Update status.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(Id))
            {
                _response = _pipeline.SendRequest(request, cancellationToken);
                var model = _response.GetJsonContent<CustomFormModel>(_options);
                if (model.IsModelComplete())
                {
                    _value = model;
                }
                return _response;
            }
        }

        /// <summary>
        /// Update status async.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(Id))
            {
                _response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                _value = await _response.GetJsonContentAsync<CustomFormModel>(_options, cancellationToken).ConfigureAwait(false);
                return _response;
            }
        }

        /// <summary>
        /// Wait for Completion.
        /// </summary>
        /// <param name="cancellationToken"></param>
        public override ValueTask<Response<CustomFormModel>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return WaitForCompletionAsync(DefaultPollingInterval, cancellationToken);
        }

        /// <summary>
        /// Wait for completion.
        /// </summary>
        /// <param name="pollingInterval"></param>
        /// <param name="cancellationToken"></param>
        public async override ValueTask<Response<CustomFormModel>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
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