// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.EventGrid.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Information about the device connection state event.
    /// </summary>
    public partial class DeviceConnectionStateEventInfo
    {
        /// <summary>
        /// Initializes a new instance of the DeviceConnectionStateEventInfo
        /// class.
        /// </summary>
        public DeviceConnectionStateEventInfo()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the DeviceConnectionStateEventInfo
        /// class.
        /// </summary>
        /// <param name="sequenceNumber">Sequence number is string
        /// representation of a hexadecimal number. string compare can be used
        /// to identify the larger number because both in ASCII and HEX numbers
        /// come after alphabets. If you are converting the string to hex, then
        /// the number is a 256 bit number.</param>
        public DeviceConnectionStateEventInfo(string sequenceNumber = default(string))
        {
            SequenceNumber = sequenceNumber;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets sequence number is string representation of a
        /// hexadecimal number. string compare can be used to identify the
        /// larger number because both in ASCII and HEX numbers come after
        /// alphabets. If you are converting the string to hex, then the number
        /// is a 256 bit number.
        /// </summary>
        [JsonProperty(PropertyName = "sequenceNumber")]
        public string SequenceNumber { get; set; }

    }
}