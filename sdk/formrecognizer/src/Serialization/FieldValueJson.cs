// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prediction;

namespace Azure.AI.FormRecognizer.Serialization
{
    internal class FieldValueJson
    {
        public static FieldValue_internal Read(JsonElement root)
        {
            var fieldValue = FieldValue_internal.Create();
            if (root.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty property in root.EnumerateObject())
                {
                    ReadPropertyValue(ref fieldValue, property);
                }
            }
            return fieldValue;
        }

        private static void ReadPropertyValue(ref FieldValue_internal fieldValue, JsonProperty property)
        {
            if (property.NameEquals("type"))
            {
                fieldValue.Type = property.Value.GetString() switch
                {
                    "string" => FieldValueType.StringType,
                    "integer" => FieldValueType.IntegerType,
                    "object" => FieldValueType.ObjectType,
                    _ => EnumJson.Read<FieldValueType>(property.Value),
                };
            }
            else if (property.NameEquals("valueString"))
            {
                fieldValue.StringValue = property.Value.GetString();
            }
            else if (property.NameEquals("valueDate"))
            {
                fieldValue.DateValue = property.Value.GetString();
            }
            else if (property.NameEquals("valueTime"))
            {
                fieldValue.TimeValue = property.Value.GetString();
            }
            else if (property.NameEquals("valuePhoneNumber"))
            {
                fieldValue.PhoneNumberValue = property.Value.GetString();
            }
            else if (property.NameEquals("valueNumber"))
            {
                fieldValue.NumberValue = property.Value.GetSingle();
            }
            else if (property.NameEquals("valueInteger"))
            {
                fieldValue.IntegerValue = property.Value.GetInt32();
            }
            else if (property.NameEquals("valueArray"))
            {
                fieldValue.ArrayValue = ArrayJson.Read(property.Value, FieldValueJson.Read);
            }
            else if (property.NameEquals("valueObject"))
            {
                fieldValue.ObjectValue = ObjectJson.Read(property.Value, FieldValueJson.Read);
            }
            else if (property.NameEquals("text"))
            {
                TextElementJson.ReadText(fieldValue, property.Value);
            }
            else if (property.NameEquals("boundingBox"))
            {
                TextElementJson.ReadBoundingBox(fieldValue, property.Value);
            }
            else if (property.NameEquals("confidence"))
            {
                fieldValue.Confidence = property.Value.GetSingle();
            }
        }
    }
}