// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.AI.FormRecognizer.Core;
using NUnit.Framework;

namespace Azure.AI.FormRecognizer.Tests.Core
{
    public class AnalyzeOptionsTests
    {
        [Test]
        public void AnalyzeOptions_Is_ImplicitBool()
        {
            AnalyzeOptions options1 = true;
            bool options2 = new AnalyzeOptions();
        }
    }
}