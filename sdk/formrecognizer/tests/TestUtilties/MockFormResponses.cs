// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.Linq;
using System.Net;
using Azure.Core;
using Azure.Core.Testing;

namespace Azure.AI.FormRecognizer.Tests.TestUtilities
{
    public static class MockFormResponses
    {
        public static MockResponse GetPageResponse(int modelCount, string nextLink)
        {
            var modelList = Enumerable.Range(1, modelCount)
                .Select((i) => @"{ ""modelInfo"": { ""modelId"": ""{modelId}"" } }"
                    .Replace("{modelId}", i.ToString(CultureInfo.InvariantCulture)));
            var content = @"{
                ""modelList"": [{modelList}],
                ""nextLink"": {nextLink}}"
                .Replace("{modelList}", String.Join(",", modelList))
                .Replace("{nextLink}", nextLink == null ? "null" : $"\"{nextLink}\"");
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            return mockResponse;
        }

        public static MockResponse GetErrorResponse(HttpStatusCode status, string code, string message)
        {
            var content = @"{ ""error"": { ""code"": ""123"", ""message"": ""foo"" } }";
            var mockResponse = new MockResponse((int)status);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            return mockResponse;
        }

        public static MockResponse GetModelResponse(string status)
        {
            var content = @"{ ""modelInfo"": { ""status"": ""{status}"" } }".Replace("{status}", status);
            var mockResponse = new MockResponse((int)HttpStatusCode.OK);
            mockResponse.AddHeader(HttpHeader.Common.JsonContentType);
            mockResponse.SetContent(content);
            return mockResponse;
        }
    }
}