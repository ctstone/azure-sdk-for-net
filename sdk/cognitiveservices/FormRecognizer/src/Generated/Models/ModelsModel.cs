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
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Response to the list custom models operation.
    /// </summary>
    public partial class ModelsModel
    {
        /// <summary>
        /// Initializes a new instance of the ModelsModel class.
        /// </summary>
        public ModelsModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ModelsModel class.
        /// </summary>
        /// <param name="summary">Summary of all trained custom models.</param>
        /// <param name="modelList">Collection of trained custom
        /// models.</param>
        /// <param name="nextLink">Link to the next page of custom
        /// models.</param>
        public ModelsModel(ModelsSummary summary = default(ModelsSummary), IList<ModelInfo> modelList = default(IList<ModelInfo>), string nextLink = default(string))
        {
            Summary = summary;
            ModelList = modelList;
            NextLink = nextLink;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets summary of all trained custom models.
        /// </summary>
        [JsonProperty(PropertyName = "summary")]
        public ModelsSummary Summary { get; set; }

        /// <summary>
        /// Gets or sets collection of trained custom models.
        /// </summary>
        [JsonProperty(PropertyName = "modelList")]
        public IList<ModelInfo> ModelList { get; set; }

        /// <summary>
        /// Gets or sets link to the next page of custom models.
        /// </summary>
        [JsonProperty(PropertyName = "nextLink")]
        public string NextLink { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Summary != null)
            {
                Summary.Validate();
            }
            if (ModelList != null)
            {
                foreach (var element in ModelList)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
        }
    }
}
