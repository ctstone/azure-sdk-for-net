// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;

namespace Azure.AI.FormRecognizer.Operations
{
    /// <summary>
    /// Models async pageable.
    /// </summary>
    public class ModelsAsyncPageable : AsyncPageable<ModelInfo>
    {
        private HttpPipeline _pipeline;
        private FormRecognizerClientOptions _options;

        internal ModelsAsyncPageable(HttpPipeline pipeline, FormRecognizerClientOptions options, CancellationToken cancellationToken)
        : base(cancellationToken)
        {
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsAsyncPageable"/> class.
        /// </summary>
        protected ModelsAsyncPageable()
        { }

        /// <summary>
        /// As pages.
        /// </summary>
        /// <param name="continuationToken"></param>
        /// <param name="pageSizeHint"></param>
        /// <returns></returns>
        public async override IAsyncEnumerable<Page<ModelInfo>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            string nextLink = null;
            do
            {
                using (var request = _pipeline.CreateListModelsRequest(nextLink))
                using (var response = await _pipeline.SendRequestAsync(request, CancellationToken).ConfigureAwait(false))
                {
                    var listing = await response.GetJsonContentAsync<ModelListing>(_options, CancellationToken).ConfigureAwait(false);
                    nextLink = listing.NextLink;
                    var page = Page<ModelInfo>.FromValues(listing.ModelList.ToList(), nextLink, response);
                    yield return page;
                }
            }
            while (!string.IsNullOrEmpty(nextLink));
        }

        /// <summary>
        /// Enumerate models asyncronously. This may make multiple service requests.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async override IAsyncEnumerator<ModelInfo> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            string nextLink = null;
            do
            {
                using (var request = _pipeline.CreateListModelsRequest(nextLink))
                using (var response = await _pipeline.SendRequestAsync(request, CancellationToken).ConfigureAwait(false))
                {
                    var listing = await response.GetJsonContentAsync<ModelListing>(_options, CancellationToken).ConfigureAwait(false);
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