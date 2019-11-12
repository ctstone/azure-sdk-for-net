// Copyright (c) Microsoft Corporation. All rights reserved.

namespace Microsoft.Azure.CognitiveServices.FormRecognizer.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Recognized field value.
    /// </summary>
    //[JsonConverter(typeof(FieldValueConverter))]
    public partial class FieldValue
    {
        /// <summary>
        /// Initializes a new instance of the FieldValue class.
        /// </summary>
        public FieldValue()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the FieldValue class.
        /// </summary>
        /// <param name="type">Type of field value. Possible values include:
        /// 'string', 'date', 'time', 'phoneNumber', 'number', 'integer',
        /// 'array', 'object'</param>
        /// <param name="valueString">String value.</param>
        /// <param name="valueDate">Date value.</param>
        /// <param name="valueTime">Time value.</param>
        /// <param name="valuePhoneNumber">Phone number value.</param>
        /// <param name="valueNumber">Floating point value.</param>
        /// <param name="valueInteger">Integer value.</param>
        /// <param name="valueArray">Array of field values.</param>
        /// <param name="valueObject">Dictionary of named field values.</param>
        /// <param name="text">Text content of the extracted field.</param>
        /// <param name="boundingBox">Bounding box of the field value, if
        /// appropriate.</param>
        /// <param name="confidence">Confidence score.</param>
        /// <param name="elements">When includeTextDetails is set to true, a
        /// list of references to the text elements constituting this
        /// field.</param>
        /// <param name="page">The 1-based page number in the input
        /// document.</param>

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets type of field value. Possible values include:
        /// 'string', 'date', 'time', 'phoneNumber', 'number', 'integer',
        /// 'array', 'object'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public FieldValueType Type { get; set; }

        /// <summary>
        /// Gets or sets string value.
        /// </summary>
        [JsonProperty(PropertyName = "valueString", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ValueString { get; set; }


        /// <summary>
        /// Gets or sets date value.
        /// </summary>
        [JsonProperty(PropertyName = "valueDate", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ValueDate { get; set; }

        /// <summary>
        /// Gets or sets time value.
        /// </summary>
        [JsonProperty(PropertyName = "valueTime", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ValueTime { get; set; }

        /// <summary>
        /// Gets or sets phone number value.
        /// </summary>
        [JsonProperty(PropertyName = "valuePhoneNumber", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ValuePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets floating point value.
        /// </summary>
        [JsonProperty(PropertyName = "valueNumber", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double ValueNumber { get; set; }

        /// <summary>
        /// Gets or sets integer value.
        /// </summary>
        [JsonProperty(PropertyName = "valueInteger", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ValueInteger { get; set; }

        /// <summary>
        /// Gets or sets array of field values.
        /// </summary>
        [JsonProperty(PropertyName = "valueArray", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<FieldValue> ValueArray { get; set; }

        /// <summary>
        /// Gets or sets dictionary of named field values.
        /// </summary>
        [JsonProperty(PropertyName = "valueObject", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IDictionary<string, FieldValue> ValueObject { get; set; }

        /// <summary>
        /// Gets or sets text content of the extracted field.
        /// </summary>
        [JsonProperty(PropertyName = "text", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets bounding box of the field value, if appropriate.
        /// </summary>
        [JsonProperty(PropertyName = "boundingBox", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public BoundingBox boundingBox { get; set; }

        /// <summary>
        /// Gets or sets confidence score.
        /// </summary>
        [JsonProperty(PropertyName = "confidence", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public double? Confidence { get; set; }


        /// <summary>
        /// Gets or sets when includeTextDetails is set to true, a list of
        /// references to the text elements constituting this field.
        /// </summary>
        [JsonProperty(PropertyName = "elements", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IList<ElementReference> Elements { get; set; }

        /// <summary>
        /// Gets or sets the 1-based page number in the input document.
        /// </summary>
        [JsonProperty(PropertyName = "pageNumber")]
        public int Page { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (ValueArray != null)
            {
                foreach (var element in ValueArray)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (ValueObject != null)
            {
                foreach (var valueElement in ValueObject.Values)
                {
                    if (valueElement != null)
                    {
                        valueElement.Validate();
                    }
                }
            }
            if (Page < 1)
            {
                throw new ValidationException(ValidationRules.InclusiveMinimum, "Page", 1);
            }
        }
    }

    public class FieldValueConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //writer.WriteNull();
            //writer.WritePropertyName("");
            //writer.WriteValue(123);
            //writer.WriteValue((value as ElementReference).RefProperty);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(FieldValue);
        }
    }
}
