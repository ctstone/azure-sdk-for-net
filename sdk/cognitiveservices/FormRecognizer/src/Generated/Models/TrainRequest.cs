// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Request parameter to train a new custom model.
    /// </summary>
    public partial class TrainRequest
    {
        /// <summary>
        /// Initializes a new instance of the TrainRequest class.
        /// </summary>
        public TrainRequest()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TrainRequest class.
        /// </summary>
        /// <param name="source">Source path containing the training
        /// documents.</param>
        /// <param name="sourceFilter">Filter to apply to the documents in the
        /// source path for training.</param>
        /// <param name="useLabelFile">Use label file for training a
        /// model.</param>
        public TrainRequest(string source, TrainSourceFilter sourceFilter = default(TrainSourceFilter), bool? useLabelFile = default(bool?))
        {
            Source = source;
            SourceFilter = sourceFilter;
            UseLabelFile = useLabelFile;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets source path containing the training documents.
        /// </summary>
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets filter to apply to the documents in the source path
        /// for training.
        /// </summary>
        [JsonProperty(PropertyName = "sourceFilter")]
        public TrainSourceFilter SourceFilter { get; set; }

        /// <summary>
        /// Gets or sets use label file for training a model.
        /// </summary>
        [JsonProperty(PropertyName = "useLabelFile")]
        public bool? UseLabelFile { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Source == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Source");
            }
            if (Source != null)
            {
                if (Source.Length > 2048)
                {
                    throw new ValidationException(ValidationRules.MaxLength, "Source", 2048);
                }
                if (Source.Length < 0)
                {
                    throw new ValidationException(ValidationRules.MinLength, "Source", 0);
                }
            }
            if (SourceFilter != null)
            {
                SourceFilter.Validate();
            }
        }
    }
}
