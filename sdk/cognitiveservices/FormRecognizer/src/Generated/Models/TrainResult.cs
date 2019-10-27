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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Custom model training result.
    /// </summary>
    public partial class TrainResult
    {
        /// <summary>
        /// Initializes a new instance of the TrainResult class.
        /// </summary>
        public TrainResult()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TrainResult class.
        /// </summary>
        /// <param name="trainingDocuments">List of the documents used to train
        /// the model and any errors reported in each document.</param>
        /// <param name="fields">List of fields used to train the model and the
        /// train operation error reported by each.</param>
        /// <param name="averageModelAccuracy">Average accuracy.</param>
        /// <param name="errors">Errors returned during the training
        /// operation.</param>
        public TrainResult(IList<TrainingDocumentInfo> trainingDocuments, IList<FormFieldsReport> fields = default(IList<FormFieldsReport>), double averageModelAccuracy = default(double), IList<FormOperationError> errors = default(IList<FormOperationError>))
        {
            TrainingDocuments = trainingDocuments;
            Fields = fields;
            AverageModelAccuracy = averageModelAccuracy;
            Errors = errors;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets list of the documents used to train the model and any
        /// errors reported in each document.
        /// </summary>
        [JsonProperty(PropertyName = "trainingDocuments")]
        public IList<TrainingDocumentInfo> TrainingDocuments { get; set; }

        /// <summary>
        /// Gets or sets list of fields used to train the model and the train
        /// operation error reported by each.
        /// </summary>
        [JsonProperty(PropertyName = "fields")]
        public IList<FormFieldsReport> Fields { get; set; }

        /// <summary>
        /// Gets or sets average accuracy.
        /// </summary>
        [JsonProperty(PropertyName = "averageModelAccuracy")]
        public double AverageModelAccuracy { get; set; }

        /// <summary>
        /// Gets or sets errors returned during the training operation.
        /// </summary>
        [JsonProperty(PropertyName = "errors")]
        public IList<FormOperationError> Errors { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (TrainingDocuments == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "TrainingDocuments");
            }
            if (TrainingDocuments != null)
            {
                foreach (var element in TrainingDocuments)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (Fields != null)
            {
                foreach (var element1 in Fields)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
            if (Errors != null)
            {
                foreach (var element2 in Errors)
                {
                    if (element2 != null)
                    {
                        element2.Validate();
                    }
                }
            }
        }
    }
}
