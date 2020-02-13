// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Runtime.Serialization;

namespace Azure.AI.FormRecognizer.Samples
{
    public class Invoice
    {
        [DataMember(Name = "InvoiceVatId")]
        public string VatId { get; set; }

        [DataMember(Name = "InvoiceCharges")]
        public string Charges { get; set; }

        [DataMember(Name = "InvoiceNumber")]
        public string Number { get; set; }

        [DataMember(Name = "InvoiceDueDate")]
        public string DueDate { get; set; }

        [DataMember(Name = "InvoiceDate")]
        public string Date { get; set; }
    }
}