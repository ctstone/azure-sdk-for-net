using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class FormRecognizerResult
    {
        public string Version { get; set; }
        public IList<ReadResult> ReadResults { get; set; }
        public IList<DocumentResult> DocumentResults { get; set; }

        public IList<TextWord> GetElementWords(IList<ElementReference> elementReferences)
        {
            return elementReferences.Select(element => ReadResults[element.PageIndex].Lines[element.LineIndex].Words[element.WordIndex]).ToArray();
        }
    }
}
