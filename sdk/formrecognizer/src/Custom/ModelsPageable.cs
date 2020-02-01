// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using System.Collections.Generic;
using System.Threading;
using Azure.AI.FormRecognizer.Extensions;
using Azure.AI.FormRecognizer.Models;
using Azure.Core.Pipeline;
using System.Net;
using System.Text.Json;

namespace Azure.AI.FormRecognizer.Custom
{
    /// <summary>
    /// A collection of custom form models that may take multiple service requests to synchronously iterate over.
    /// </summary>
    internal class ModelsPageable : Pageable<FormRecognizerCustomModelInfo>
    {
        private HttpPipeline _pipeline;
        private JsonSerializerOptions _options;

        internal ModelsPageable(HttpPipeline pipeline, JsonSerializerOptions options, CancellationToken cancellationToken)
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

        /// <inheritdoc />
        public override IEnumerable<Page<FormRecognizerCustomModelInfo>> AsPages(string continuationToken = null, int? pageSizeHint = null)
        {
            Page<FormRecognizerCustomModelInfo> page;
            do
            {
                page = GetPage(continuationToken);
                yield return page;

            }
            while (!string.IsNullOrEmpty(page.ContinuationToken));
        }

        /// <summary>
        /// Get a page of models.
        /// </summary>
        /// <param name="continuationToken"></param>
        public Page<FormRecognizerCustomModelInfo> GetPage(string continuationToken = null)
        {
            using (var request = _pipeline.CreateListModelsRequest(continuationToken))
            using (var response = _pipeline.SendRequest(request, CancellationToken))
            {
                response.ExpectStatus(HttpStatusCode.OK, _options);
                var listing = response.GetJsonContent<ModelListing>(_options);
                return Page<FormRecognizerCustomModelInfo>.FromValues(listing.ModelList.ToList(), listing.NextLink, response);
            }
        }

        /// <inheritdoc />
        public override IEnumerator<FormRecognizerCustomModelInfo> GetEnumerator()
        {
            string nextLink = null;
            do
            {
                using (var request = _pipeline.CreateListModelsRequest(nextLink))
                using (var response = _pipeline.SendRequest(request, CancellationToken))
                {
                    response.ExpectStatus(HttpStatusCode.OK, _options);
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