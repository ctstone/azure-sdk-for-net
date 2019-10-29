using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using System;
using System.IO;
using Xunit.Sdk;
using System.Net.Http;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using System.Collections.Generic;

namespace FormRecognizerSDK.Tests
{
    public class FormRecognizerTests : BaseTests
    {
        ITestOutputHelper _output;
        public FormRecognizerTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void VerifyFormClientObjectCreation()
        {
            var client = GetFormRecognizerClient(null);
            
            Assert.True(client.GetType() == typeof(FormRecognizerClient));
        }

        [Fact(Skip = "not completed")]
        public void FormRecognizerSDK_AnalyzeWithCustomModelAsync()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                HttpMockServer.Initialize(this.GetType(), "FormRecognizerSDK_AnalyzeWithCustomModelAsync");

                using (IFormRecognizerClient client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
                {
                    var result = client.AnalyzeWithCustomModelAsync(new Guid(), false, stream).Result;
                }
            }
        }

        [Fact]
        public void FormRecognizerSDK_AnalyzeReceiptAsync()
        {
            using (MockContext context = MockContext.Start(this.GetType()))
            {
                HttpMockServer.Initialize(this.GetType(), "FormRecognizerSDK_AnalyzeReceiptAsync");

                using (IFormRecognizerClient client = GetFormRecognizerClient(HttpMockServer.CreateInstance()))
                using (FileStream stream = new FileStream(GetTestImagePath("Receipt_003_934.jpg"), FileMode.Open))
                {

                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();
                    var result = client.AnalyzeReceiptAsync(text).Result;
                    _output.WriteLine(JsonConvert.SerializeObject(result));

                }
            }
        }
    }
}
