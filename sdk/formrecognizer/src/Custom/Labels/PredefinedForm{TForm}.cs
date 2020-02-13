// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Prebuilt;

namespace Azure.AI.FormRecognizer.Custom.Labels
{
    /// <inheritdoc />
    public class PredefinedForm<TForm> : PredefinedForm
        where TForm : new()
    {
        private const BindingFlags PropertyBindingFlags
            = BindingFlags.Public
            | BindingFlags.Instance
            | BindingFlags.IgnoreCase;

        /// <summary>
        /// Get the typed form value.
        /// </summary>
        public TForm Form { get; }

        internal PredefinedForm(PredefinedFormInternal field)
            : base(field)
        {
            // TODO: this can probably be optimized to avoid boxing.
            Form = (TForm)GetPredefinedFieldObject(Fields, typeof(TForm));
        }

        private object GetPredefinedFieldObject(IDictionary<string, PredefinedField> objectValue, Type type)
        {
            var obj = Activator.CreateInstance(type);
            var properties = type.GetProperties(PropertyBindingFlags);
            foreach (var kvp in objectValue)
            {
                var childFieldName = kvp.Key;
                var childField = kvp.Value;
                var childProperty = properties
                    .FirstOrDefault((x) =>
                    {
                        var dataMember = x.GetCustomAttribute<DataMemberAttribute>(true);
                        return dataMember?.Name == childFieldName || x.Name.Equals(childFieldName, StringComparison.OrdinalIgnoreCase);
                    });
                if (childProperty != default && childProperty.CanWrite)
                {
                    var childValue = GetPredefinedFieldValue(childField, childProperty.PropertyType);
                    childProperty.SetValue(obj, childValue);
                }
            }
            return obj;
        }

        private object GetPredefinedFieldValue(PredefinedField field, Type type)
        {
            if (field.Type == PredefinedFieldType.ObjectType)
            {
                return GetPredefinedFieldObject(field.ObjectValue, type);
            }
            else if (field.Type == PredefinedFieldType.Array)
            {
                if (typeof(IList).IsAssignableFrom(type))
                {
                    var arrayElementType = type.GetElementType();
                    if (arrayElementType != default)
                    {
                        var fieldItems = field.ArrayValue;
                        var array = Activator.CreateInstance(type, fieldItems.Length) as IList;
                        for (var i = 0; i < fieldItems.Length; i += 1)
                        {
                            array[i] = GetPredefinedFieldValue(fieldItems[i], arrayElementType);
                        }
                        return array;
                    }

                }
            }
            else if (field.Type == PredefinedFieldType.StringType)
            {
                return field.StringValue;
            }
            else if (field.Type == PredefinedFieldType.IntegerType)
            {
                return field.IntegerValue;
            }
            else if (field.Type == PredefinedFieldType.Number)
            {
                return field.NumberValue;
            }
            else if (field.Type == PredefinedFieldType.Date)
            {
                return field.DateValue;
            }
            else if (field.Type == PredefinedFieldType.PhoneNumber)
            {
                return field.PhoneNumberValue;
            }

            // no match, return defaults
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}