// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Arguments;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// Represents a long-running training operation.
    /// </summary>
    public class TrainingOperation : Operation<CustomFormModel>
    {
        private const string LocationHeader = "Location";
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(10);
        private readonly string _id;
        private readonly HttpPipeline _pipeline;
        private readonly JsonSerializerOptions _options;
        private CustomFormModel _value;
        private Response _response;

        /// <summary>
        /// Get the ID of the training operation. This value can be used to poll for the status of the training outcome.
        /// </summary>
        public override string Id => _id;

        /// <summary>
        /// The final result of the training operation, if the operation completed successfully.
        /// </summary>
        public override CustomFormModel Value => HasValue ? _value : default;

        /// <summary>
        /// True if the training operation completed.
        /// </summary>
        public override bool HasCompleted => _value?.IsModelComplete() ?? false;

        /// <summary>
        /// True if the training operation completed successfully.
        /// </summary>
        public override bool HasValue => _value?.IsModelSuccess() ?? false;

        internal TrainingOperation(HttpPipeline pipeline, string operationId, JsonSerializerOptions options)
        {
            Throw.IfMissing(pipeline, nameof(pipeline));
            Throw.IfMissing(operationId, nameof(operationId));
            Throw.IfMissing(options, nameof(options));
            Throw.IfNullOrEmpty(operationId, nameof(operationId));
            _id = operationId;
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingOperation"/> class for mocking.
        /// </summary>
        protected TrainingOperation()
        { }

        /// <inheritdoc/>
        public override Response GetRawResponse()
        {
            return _response;
        }

        /// <inheritdoc/>
        public override Response UpdateStatus(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(Id, includeKeys: true))
            {
                return UpdateStatus(_pipeline.SendRequest(request, cancellationToken));
            }
        }

        /// <inheritdoc/>
        public override async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateGetModelRequest(Id, includeKeys: true))
            {
                return UpdateStatus(await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false));
            }
        }

        /// <inheritdoc/>
        public override ValueTask<Response<CustomFormModel>> WaitForCompletionAsync(CancellationToken cancellationToken = default)
        {
            return WaitForCompletionAsync(DefaultPollingInterval, cancellationToken);
        }

        /// <inheritdoc/>
        public async override ValueTask<Response<CustomFormModel>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken = default)
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
            var model = response.GetJsonContent<CustomFormModel>(_options);
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