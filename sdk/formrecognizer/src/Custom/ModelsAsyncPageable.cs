// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// A collection of custom form models that may take multiple service requests to asynchronously iterate over.
    /// </summary>
    public class ModelsAsyncPageable : AsyncPageable<ModelInfo>
    {
        private HttpPipeline _pipeline;
        private JsonSerializerOptions _options;

        internal ModelsAsyncPageable(HttpPipeline pipeline, JsonSerializerOptions options, CancellationToken cancellationToken)
        : base(cancellationToken)
        {
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsAsyncPageable"/> class for mocking.
        /// </summary>
        protected ModelsAsyncPageable()
        { }

        /// <inheritdoc />
        public async override IAsyncEnumerable<Page<ModelInfo>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            Page<ModelInfo> page;
            do
            {
                page = await GetPageAsync(continuationToken, CancellationToken).ConfigureAwait(false);
                yield return page;
            }
            while (!string.IsNullOrEmpty(page.ContinuationToken));
        }

        /// <summary>
        /// Get a page of models.
        /// </summary>
        /// <param name="continuationToken">Optional continuation token from a previous page result.</param>
        /// <param name="cancellationToken">Optional cancellation token.</param>
        public async Task<Page<ModelInfo>> GetPageAsync(string continuationToken = null, CancellationToken cancellationToken = default)
        {
            using (var request = _pipeline.CreateListModelsRequest(continuationToken))
            using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var listing = await response.GetJsonContentAsync<ModelListing>(_options, cancellationToken).ConfigureAwait(false);
                return Page<ModelInfo>.FromValues(listing.ModelList.ToList(), listing.NextLink, response);
            }
        }

        /// <inheritdoc />
        public async override IAsyncEnumerator<ModelInfo> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            string nextLink = null;
            do
            {
                using (var request = _pipeline.CreateListModelsRequest(nextLink))
                using (var response = await _pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false))
                {
                    response.ExpectStatus(HttpStatusCode.OK, _options);
                    var listing = await response.GetJsonContentAsync<ModelListing>(_options, cancellationToken).ConfigureAwait(false);
                    nextLink = listing.NextLink;
                    foreach (var model in listing.ModelList)
                    {
                        yield return model;
                    }
                }
            }
            while (!string.IsNullOrEmpty(nextLink));
        }
    }
}