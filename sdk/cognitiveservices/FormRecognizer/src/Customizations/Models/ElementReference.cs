using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    public class ElementReference
    {
        public ElementReference(string refProperty)
        {
            RefProperty = refProperty;
        }

        private int _pageIndex;
        private int _lineIndex;
        private int _wordIndex;
        private string _resolve = null;

        public string RefProperty { get; set; }

        [JsonIgnore]
        public int PageIndex { get { Resolve(); return _pageIndex; } }

        [JsonIgnore]
        public int LineIndex { get { Resolve(); return _lineIndex; } }

        [JsonIgnore]
        public int WordIndex { get { Resolve(); return _wordIndex; } }

        private void Resolve()
        {
            if (_resolve != RefProperty)
            {
                var match = Regex.Match(RefProperty, @"^#/readResults/(\d+)/lines/(\d+)/words/(\d+)$");
                if (!match.Success)
                {
                    throw new ArgumentException("Invalid element reference.");
                }
                _pageIndex = int.Parse(match.Groups[1].Value);
                _lineIndex = int.Parse(match.Groups[2].Value);
                _wordIndex = int.Parse(match.Groups[3].Value);
                _resolve = RefProperty;
            }
        }

    }
}