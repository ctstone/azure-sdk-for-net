// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Receipt extraction item.
    /// </summary>
    public class ReceiptExtractionItem
    {
        private const string QuantityKey = "Quantity";
        private const string NameKey = "Name";
        private const string TotalPriceKey = "TotalPrice";

        /// <summary>
        /// Get the receipt item quantity.
        /// </summary>
        public PredefinedField<float> Quantity { get; }

        /// <summary>
        /// Get the receipt item name.
        /// </summary>
        public PredefinedField<string> Name { get; }

        /// <summary>
        /// Get the receipt item total price.
        /// </summary>
        public PredefinedField<float> TotalPrice { get; }

        internal ReceiptExtractionItem(PredefinedField field)
        {
            if (field.Type == PredefinedFieldType.ObjectType)
            {
                foreach (var kvp in field.ObjectValue)
                {
                    var key = kvp.Key;
                    var value = kvp.Value;
                    if (key == QuantityKey)
                    {
                        Quantity = new PredefinedField<float>(value.NumberValue.Value, value);
                    }
                    else if (key == NameKey)
                    {
                        Name = new PredefinedField<string>(value.StringValue, value);
                    }
                    else if (key == TotalPriceKey)
                    {
                        TotalPrice = new PredefinedField<float>(value.NumberValue.Value, value);
                    }
                }
            }
        }
    }
}