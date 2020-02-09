// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary>
    /// Form analysis
    /// </summary>
    public class ReceiptExtraction
    {
        private const string ReceiptTypeKey = "ReceiptType";
        private const string MerchantNameKey = "MerchantName";
        private const string MerchantAddressKey = "MerchantAddress";
        private const string MerchantPhoneNumberKey = "MerchantPhoneNumber";
        private const string TransactionDateKey = "TransactionDateKey";
        private const string TransactionTimeKey = "TransactionTimeKey";
        private const string ItemsKey = "ItemsKey";
        private const string SubtotalKey = "Subtotal";
        private const string TaxKey = "TaxKey";
        private const string TipKey = "TipKey";
        private const string TotalKey = "TotalKey";

        private readonly IDictionary<string, PredefinedField> _fields;

        /// <summary>
        /// Get the receipt type.
        /// </summary>
        public PredefinedField<string> ReceiptType { get; }

        /// <summary>
        /// Get the receipt merchant name.
        /// </summary>
        public PredefinedField<string> MerchantName { get; }

        /// <summary>
        /// Get the receipt merchant address.
        /// </summary>
        public PredefinedField<string> MerchantAddress { get; }

        /// <summary>
        /// Get the receipt merchant phone number.
        /// </summary>
        public PredefinedField<string> MerchantPhoneNumber { get; }

        /// <summary>
        /// Get the receipt transaction date.
        /// </summary>
        public PredefinedField<DateTimeOffset> TransactionDate { get; }

        /// <summary>
        /// Get the receipt transaction time.
        /// </summary>
        public PredefinedField<string> TransactionTime { get; }

        /// <summary>
        /// Get the receipt items.
        /// </summary>
        public PredefinedField<ReceiptExtractionItem[]> Items { get; }

        /// <summary>
        /// Get the receipt subtotal.
        /// </summary>
        public PredefinedField<float> Subtotal { get; }

        /// <summary>
        /// Get the receipt tax.
        /// </summary>
        public PredefinedField<float> Tax { get; }

        /// <summary>
        /// Get the receipt tip.
        /// </summary>
        public PredefinedField<float> Tip { get; }

        /// <summary>
        /// Get the receipt total.
        /// </summary>
        public PredefinedField<float> Total { get; }

        internal ReceiptExtraction(IDictionary<string, PredefinedField> fields)
        {
            _fields = fields;
            foreach (var kvp in fields)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                if (kvp.Key == ReceiptTypeKey)
                {
                    ReceiptType = new PredefinedField<string>(value.StringValue, value);
                }
                else if (key == MerchantNameKey)
                {
                    MerchantName = new PredefinedField<string>(value.StringValue, value);
                }
                else if (key == MerchantAddressKey)
                {
                    MerchantAddress = new PredefinedField<string>(value.StringValue, value);
                }
                else if (key == MerchantPhoneNumberKey)
                {
                    MerchantPhoneNumber = new PredefinedField<string>(value.StringValue, value);
                }
                else if (key == TransactionDateKey)
                {
                    TransactionDate = new PredefinedField<DateTimeOffset>(value.DateValue, value);
                }
                else if (key == TransactionTimeKey)
                {
                    TransactionTime = new PredefinedField<string>(value.StringValue, value);
                }
                else if (key == ItemsKey)
                {
                    var items = value.ArrayValue
                        .Select((x) => new ReceiptExtractionItem(x))
                        .ToArray();
                    Items = new PredefinedField<ReceiptExtractionItem[]>(items, value);
                }
                else if (key == SubtotalKey)
                {
                    Subtotal = new PredefinedField<float>(value.NumberValue.Value, value);
                }
                else if (key == TaxKey)
                {
                    Tax = new PredefinedField<float>(value.NumberValue.Value, value);
                }
                else if (key == TipKey)
                {
                    Tip = new PredefinedField<float>(value.NumberValue.Value, value);
                }
                else if (key == TotalKey)
                {
                    Total = new PredefinedField<float>(value.NumberValue.Value, value);
                }
            }

            if (Items == default)
            {
                Items = new PredefinedField<ReceiptExtractionItem[]>(Array.Empty<ReceiptExtractionItem>(), null);
            }
        }

        /// <summary>
        /// Try to get a predefined field by name, if it exists.
        /// </summary>
        /// <param name="name">Field name.</param>
        /// <param name="value">Field value.</param>
        public bool TryGetField(string name, out PredefinedField value)
        {
            return _fields.TryGetValue(name, out value);
        }
    }
}