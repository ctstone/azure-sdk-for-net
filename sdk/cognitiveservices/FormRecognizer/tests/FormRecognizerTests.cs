using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Xunit;
using System.IO;

namespace FormRecognizerSDK.Tests
{
    public class FormRecognizerTests : BaseTests
    {
        [Fact]
        public void Test_ApiKey_Endpoint_Overload()
        {
            var client = new FormRecognizerClient("fake-key", "http://example.org");
        }
    }
}
