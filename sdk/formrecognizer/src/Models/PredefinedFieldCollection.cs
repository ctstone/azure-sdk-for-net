// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Azure.AI.FormRecognizer.Prediction
{
    /// <summary>
    /// </summary>
    public class PredefinedFieldCollection : Collection<PredefinedField>
    {
        /// <summary>
        /// Dictionary of named field values, where the key is the name of the label
        /// for the form field specified during training time, and the FieldValue is the value
        /// read from the form field.
        /// </summary>
        private IDictionary<string, PredefinedFieldValue> _fieldsByName { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public PredefinedFieldValue GetFieldValue(string fieldName)
        {
            return _fieldsByName[fieldName];
        }

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public string GetStringFieldValue(string fieldName)
        {
            PredefinedFieldValue fieldValue = _fieldsByName[fieldName];

            if (fieldValue.Type != FieldValueType.StringType)
            {
                throw new InvalidOperationException($"Value of {fieldName} is not a string type.");
            }

            return fieldValue.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public int GetIntFieldValue(string fieldName)
        {
            PredefinedFieldValue fieldValue = _fieldsByName[fieldName];

            if (fieldValue.Type != FieldValueType.IntegerType)
            {
                throw new InvalidOperationException($"Value of {fieldName} is not an int type.");
            }

            return fieldValue.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public float GetFloatFieldValue(string fieldName)
        {
            PredefinedFieldValue fieldValue = _fieldsByName[fieldName];

            if (fieldValue.Type != FieldValueType.Number)
            {
                throw new InvalidOperationException($"Value of {fieldName} is not a float type.");
            }

            return fieldValue.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public PredefinedFieldValue[] GetFieldValueArray(string fieldName)
        {
            PredefinedFieldValue fieldValue = _fieldsByName[fieldName];

            if (fieldValue.Type != FieldValueType.Array)
            {
                throw new InvalidOperationException($"Value of {fieldName} is not an array type.");
            }

            return fieldValue.Value;
        }

        /// <summary>
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public PredefinedFieldCollection GetFieldCollection(string fieldName)
        {
            PredefinedFieldValue fieldValue = _fieldsByName[fieldName];

            if (fieldValue.Type != FieldValueType.Array)
            {
                throw new InvalidOperationException($"Value of {fieldName} is not a field collection type.");
            }

            return fieldValue.Value;
        }
    }
}
