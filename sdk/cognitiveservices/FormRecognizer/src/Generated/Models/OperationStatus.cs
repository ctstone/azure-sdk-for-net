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
    using Newtonsoft.Json.Converters;
    using System.Runtime;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for OperationStatus.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OperationStatus
    {
        [EnumMember(Value = "notStarted")]
        NotStarted,
        [EnumMember(Value = "running")]
        Running,
        [EnumMember(Value = "succeeded")]
        Succeeded,
        [EnumMember(Value = "failed")]
        Failed
    }
    internal static class OperationStatusEnumExtension
    {
        internal static string ToSerializedValue(this OperationStatus? value)
        {
            return value == null ? null : ((OperationStatus)value).ToSerializedValue();
        }

        internal static string ToSerializedValue(this OperationStatus value)
        {
            switch( value )
            {
                case OperationStatus.NotStarted:
                    return "notStarted";
                case OperationStatus.Running:
                    return "running";
                case OperationStatus.Succeeded:
                    return "succeeded";
                case OperationStatus.Failed:
                    return "failed";
            }
            return null;
        }

        internal static OperationStatus? ParseOperationStatus(this string value)
        {
            switch( value )
            {
                case "notStarted":
                    return OperationStatus.NotStarted;
                case "running":
                    return OperationStatus.Running;
                case "succeeded":
                    return OperationStatus.Succeeded;
                case "failed":
                    return OperationStatus.Failed;
            }
            return null;
        }
    }
}
