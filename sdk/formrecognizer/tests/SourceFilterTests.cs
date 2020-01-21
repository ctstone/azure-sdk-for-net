// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Xunit;

namespace Azure.AI.FormRecognizer.Tests
{
    public class SourceFilterTests
    {
        [Fact]
        public void Filter_Prefix_IsOptional()
        {
            var request = new SourceFilter { Prefix = null };
        }

        [Fact]
        public void Request_IncludeSubFolders_IsOptional()
        {
            var request = new SourceFilter { IncludeSubFolders = null };
        }
    }
}