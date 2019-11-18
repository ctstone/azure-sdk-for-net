// Copyright (c) Microsoft Corporation. All rights reserved.

using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public partial class AnalyzeResult
    {
        public IList<TextWord> GetElementWords(IList<ElementReference> elementReferences)
        {
            return elementReferences.Select((element) => ReadResults[element.PageIndex].Lines[element.LineIndex].Words[element.WordIndex]).ToArray();
        }

        public bool ShouldSerializeReadResults()
        {
            return (ReadResults != null);
        }

        public bool ShouldSerializePageResults()
        {
            return (PageResults != null);
        }

        public bool ShouldSerializeDocumentResults()
        {
            return (DocumentResults != null);
        }

        public bool ShouldSerializeErrors()
        {
            return (Errors != null);
        }
    }
}