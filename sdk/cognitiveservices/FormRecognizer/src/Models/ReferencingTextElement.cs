// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Base class for extracted text elements.
    /// </summary>
    public abstract class ReferencingTextElement : TextElement
    {
        private const string SegmentReadResults = "readResults";
        private const string SegmentLines = "lines";
        private const string SegmentWords = "words";
        /// <summary>
        /// When includeTextDetails is set to true, a list of references to the text elements constituting this value.
        /// </summary>
        public TextElement[] Elements { get; private set; }

        internal string[] ElementReferences { get; set; }

        /// <summary>
        /// Resolve references in the form of `"#/readResults/2/lines/3/words/12` to the corresponding
        /// <see cref="TextElement" /> relative to a given <see cref="ReadResult" />.
        ///
        /// Updated values will be accessible from the public `Elements` property.
        ///
        /// This method should be called _after_ `ElementReferences` is set, but _before_ the object is
        /// returned to the caller.
        /// </summary>
        /// <param name="readResults">The top-level OCR read results.</param>
        internal void ResolveTextReferences(ReadResult[] readResults)
        {
            if (ElementReferences == default)
            {
                ElementReferences = Array.Empty<string>();
            }
            Elements = new TextElement[ElementReferences.Length];
            for (var i = 0; i < ElementReferences.Length; i += 1)
            {
                Elements[i] = ResolveTextReference(readResults, ElementReferences[i]);
            }
        }

        internal static TextElement ResolveTextReference(ReadResult[] results, string reference)
        {
            TextElement textElement = null;
            ReadResult readResult = null;
            if (!string.IsNullOrEmpty(reference) && reference.Length > 2 && reference[0] == '#')
            {
                // offset by 2 to skip the '#/' prefix
                var segments = reference.Substring(2).Split('/');

                // must have an even number of segments
                if (segments.Length % 2 == 0)
                {
                    int offset;
                    for (var i = 0; i < segments.Length; i += 2)
                    {
                        // the next segment must be an integer
                        if (int.TryParse(segments[i + 1], out offset))
                        {
                            var segment = segments[i];

                            // this is the root page element
                            if (segment == SegmentReadResults)
                            {
                                readResult = results[offset];
                            }

                            // this is a text element
                            else if (readResult != default)
                            {
                                if (segment == SegmentLines)
                                {
                                    textElement = readResult.Lines[offset];
                                }
                                else if (segment == SegmentWords && textElement is TextLine)
                                {
                                    textElement = (textElement as TextLine).Words[offset];
                                }
                            }
                        }
                    }
                }
            }
            return textElement;
        }
    }
}