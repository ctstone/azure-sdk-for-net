// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text;
using Azure.Core;
using Xunit;
using static Azure.AI.FormRecognizer.FormClientOptions;

namespace Azure.AI.FormRecognizer.Tests
{
    public class TrainingRequestTests
    {
        [Fact]
        public void Request_SourceFilter_IsOptional()
        {
            var request = new TrainingRequest { Filter = null };
        }

        [Fact]
        public void Request_UseLabelFile_IsOptional()
        {
            var request = new TrainingRequest { UseLabelFile = null };
        }
    }
}