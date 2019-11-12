// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Analyze operation result.
    /// </summary>
    public partial class AnalyzeResult
    {
        /// <summary>
        /// Initializes a new instance of the AnalyzeResult class.
        /// </summary>
        public AnalyzeResult()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the AnalyzeResult class.
        /// </summary>
        /// <param name="version">Version of schema used for this
        /// result.</param>
        /// <param name="readResults">Text extracted from the input.</param>
        /// <param name="pageResults">Page-level information extracted from the
        /// input.</param>
        /// <param name="documentResults">Document-level information extracted
        /// from the input.</param>
        /// <param name="errors">List of errors reported during the analyze
        /// operation.</param>
        public AnalyzeResult(string version, IList<ReadResult> readResults, IList<PageResult> pageResults = default(IList<PageResult>), IList<DocumentResult> documentResults = default(IList<DocumentResult>), IList<FormOperationError> errors = default(IList<FormOperationError>))
        {
            Version = version;
            ReadResults = readResults;
            PageResults = pageResults;
            DocumentResults = documentResults;
            Errors = errors;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets version of schema used for this result.
        /// </summary>
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets text extracted from the input.
        /// </summary>
        [JsonProperty(PropertyName = "readResults")]
        public IList<ReadResult> ReadResults { get; set; }
        public bool ShouldSerializeReadResults()
        {
            return (ReadResults != null);
        }

        /// <summary>
        /// Gets or sets page-level information extracted from the input.
        /// </summary>
        [JsonProperty(PropertyName = "pageResults")]
        public IList<PageResult> PageResults { get; set; }
        public bool ShouldSerializePageResults()
        {
            return (PageResults != null);
        }

        /// <summary>
        /// Gets or sets document-level information extracted from the input.
        /// </summary>
        [JsonProperty(PropertyName = "documentResults")]
        public IList<DocumentResult> DocumentResults { get; set; }
        public bool ShouldSerializeDocumentResults()
        {
            return (DocumentResults != null);
        }

        /// <summary>
        /// Gets or sets list of errors reported during the analyze operation.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public IList<FormOperationError> Errors { get; set; }
        public bool ShouldSerializeErrors()
        {
            return (Errors != null);
        }

        public IList<TextWord> GetElementWords(IList<ElementReference> elementReferences)
        {
            return elementReferences.Select(element => ReadResults[element.PageIndex].Lines[element.LineIndex].Words[element.WordIndex]).ToArray();
        }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Version == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Version");
            }
            if (ReadResults == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "ReadResults");
            }
            if (ReadResults != null)
            {
                foreach (var element in ReadResults)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (PageResults != null)
            {
                foreach (var element1 in PageResults)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
            if (DocumentResults != null)
            {
                foreach (var element2 in DocumentResults)
                {
                    if (element2 != null)
                    {
                        element2.Validate();
                    }
                }
            }
            if (Errors != null)
            {
                foreach (var element3 in Errors)
                {
                    if (element3 != null)
                    {
                        element3.Validate();
                    }
                }
            }
        }
    }
}
