using Microsoft.Azure.CognitiveServices.FormRecognizer;
using Microsoft.Azure.CognitiveServices.FormRecognizer.Models;
using Microsoft.Azure.Test.HttpRecorder;
using Microsoft.Rest.ClientRuntime.Azure.TestFramework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;
using System;
using System.IO;


namespace FormRecognizerSDK.Tests
{
    public class FormRecognizerTests : BaseTests
    {
        private ITestOutputHelper _output;

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
                    // TODO - modify input parameter for AnalyzeReceiptAsync after overload SDK
                    StreamReader reader = new StreamReader(stream);
                    string text = reader.ReadToEnd();
                    var result = client.AnalyzeReceiptAsync(text).Result;
                    var expectedResultJson = JObject.Parse(File.ReadAllText(GetTestImagePath("Receipt_003_934.json")));
                    var expectedResult = JsonConvert.DeserializeObject<AnalyzeResult>(expectedResultJson.ToString());
                
                    // TODO - check if result match expectation
                }
            }
        }
    }
}
