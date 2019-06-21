// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Security.Models
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Contact details for security issues
    /// </summary>
    [Rest.Serialization.JsonTransformation]
    public partial class SecurityContact : Resource
    {
        /// <summary>
        /// Initializes a new instance of the SecurityContact class.
        /// </summary>
        public SecurityContact()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the SecurityContact class.
        /// </summary>
        /// <param name="email">The email of this security contact</param>
        /// <param name="alertNotifications">Whether to send security alerts
        /// notifications to the security contact. Possible values include:
        /// 'On', 'Off'</param>
        /// <param name="alertsToAdmins">Whether to send security alerts
        /// notifications to subscription admins. Possible values include:
        /// 'On', 'Off'</param>
        /// <param name="id">Resource Id</param>
        /// <param name="name">Resource name</param>
        /// <param name="type">Resource type</param>
        /// <param name="phone">The phone number of this security
        /// contact</param>
        public SecurityContact(string email, string alertNotifications, string alertsToAdmins, string id = default(string), string name = default(string), string type = default(string), string phone = default(string))
            : base(id, name, type)
        {
            Email = email;
            Phone = phone;
            AlertNotifications = alertNotifications;
            AlertsToAdmins = alertsToAdmins;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets the email of this security contact
        /// </summary>
        [JsonProperty(PropertyName = "properties.email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of this security contact
        /// </summary>
        [JsonProperty(PropertyName = "properties.phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets whether to send security alerts notifications to the
        /// security contact. Possible values include: 'On', 'Off'
        /// </summary>
        [JsonProperty(PropertyName = "properties.alertNotifications")]
        public string AlertNotifications { get; set; }

        /// <summary>
        /// Gets or sets whether to send security alerts notifications to
        /// subscription admins. Possible values include: 'On', 'Off'
        /// </summary>
        [JsonProperty(PropertyName = "properties.alertsToAdmins")]
        public string AlertsToAdmins { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Email == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Email");
            }
            if (AlertNotifications == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "AlertNotifications");
            }
            if (AlertsToAdmins == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "AlertsToAdmins");
            }
        }
    }
}