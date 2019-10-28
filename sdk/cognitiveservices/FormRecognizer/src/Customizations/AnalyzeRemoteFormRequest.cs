using Newtonsoft.Json;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer
{
    public class AnalyzeRemoteFormRequest
    {
        [JsonProperty("source")]
        public string Source { get; set; }
    }
}