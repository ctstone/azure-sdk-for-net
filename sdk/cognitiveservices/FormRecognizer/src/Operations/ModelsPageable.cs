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
    public class ModelsPageable : Pageable<ModelInfo>
    {
        private HttpPipeline _pipeline;
        private FormRecognizerClientOptions _options;

        internal ModelsPageable(HttpPipeline pipeline, FormRecognizerClientOptions options, CancellationToken cancellationToken)
        : base(cancellationToken)
        {
            _pipeline = pipeline;
            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelsAsyncPageable"/> class.
        /// </summary>
        protected ModelsPageable()
        { }

        /// <summary>
        /// As pages.
        /// </summary>
        /// <param name="continuationToken"></param>
        /// <param name="pageSizeHint"></param>
        /// <returns></returns>
        public override IEnumerable<Page<ModelInfo>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            string nextLink = null;
            do
            {
                using (var request = _pipeline.CreateListModelsRequest(nextLink))
                using (var response = _pipeline.SendRequest(request, CancellationToken))
                {
                    var listing = response.GetJsonContent<ModelListing>(_options);
                    nextLink = listing.NextLink;
                    var page = Page<ModelInfo>.FromValues(listing.ModelList.ToList(), nextLink, response);
                    yield return page;
                }
            }
            while (!string.IsNullOrEmpty(nextLink));
        }

        /// <summary>
        /// Enumerate models. This may make multiple service requests.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator<ModelInfo> GetEnumerator()
        {
            string nextLink = null;
            do
            {
                using (var request = _pipeline.CreateListModelsRequest(nextLink))
                using (var response = _pipeline.SendRequest(request, CancellationToken))
                {
                    var listing = response.GetJsonContent<ModelListing>(_options);
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