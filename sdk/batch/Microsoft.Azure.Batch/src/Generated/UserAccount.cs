// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

//
// This file was autogenerated by a tool.
// Do not modify it.
//

namespace Microsoft.Azure.Batch
{
    using Models = Microsoft.Azure.Batch.Protocol.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A user account to create on an Azure Batch node. Tasks may be configured to execute in the security context of the 
    /// user account.
    /// </summary>
    public partial class UserAccount : ITransportObjectProvider<Models.UserAccount>, IPropertyMetadata
    {
        private class PropertyContainer : PropertyCollection
        {
            public readonly PropertyAccessor<Common.ElevationLevel?> ElevationLevelProperty;
            public readonly PropertyAccessor<LinuxUserConfiguration> LinuxUserConfigurationProperty;
            public readonly PropertyAccessor<string> NameProperty;
            public readonly PropertyAccessor<string> PasswordProperty;
            public readonly PropertyAccessor<WindowsUserConfiguration> WindowsUserConfigurationProperty;

            public PropertyContainer() : base(BindingState.Unbound)
            {
                this.ElevationLevelProperty = this.CreatePropertyAccessor<Common.ElevationLevel?>(nameof(ElevationLevel), BindingAccess.Read | BindingAccess.Write);
                this.LinuxUserConfigurationProperty = this.CreatePropertyAccessor<LinuxUserConfiguration>(nameof(LinuxUserConfiguration), BindingAccess.Read | BindingAccess.Write);
                this.NameProperty = this.CreatePropertyAccessor<string>(nameof(Name), BindingAccess.Read | BindingAccess.Write);
                this.PasswordProperty = this.CreatePropertyAccessor<string>(nameof(Password), BindingAccess.Read | BindingAccess.Write);
                this.WindowsUserConfigurationProperty = this.CreatePropertyAccessor<WindowsUserConfiguration>(nameof(WindowsUserConfiguration), BindingAccess.Read | BindingAccess.Write);
            }

            public PropertyContainer(Models.UserAccount protocolObject) : base(BindingState.Bound)
            {
                this.ElevationLevelProperty = this.CreatePropertyAccessor(
                    UtilitiesInternal.MapNullableEnum<Models.ElevationLevel, Common.ElevationLevel>(protocolObject.ElevationLevel),
                    nameof(ElevationLevel),
                    BindingAccess.Read);
                this.LinuxUserConfigurationProperty = this.CreatePropertyAccessor(
                    UtilitiesInternal.CreateObjectWithNullCheck(protocolObject.LinuxUserConfiguration, o => new LinuxUserConfiguration(o).Freeze()),
                    nameof(LinuxUserConfiguration),
                    BindingAccess.Read);
                this.NameProperty = this.CreatePropertyAccessor(
                    protocolObject.Name,
                    nameof(Name),
                    BindingAccess.Read);
                this.PasswordProperty = this.CreatePropertyAccessor(
                    protocolObject.Password,
                    nameof(Password),
                    BindingAccess.Read);
                this.WindowsUserConfigurationProperty = this.CreatePropertyAccessor(
                    UtilitiesInternal.CreateObjectWithNullCheck(protocolObject.WindowsUserConfiguration, o => new WindowsUserConfiguration(o).Freeze()),
                    nameof(WindowsUserConfiguration),
                    BindingAccess.Read);
            }
        }

        private readonly PropertyContainer propertyContainer;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccount"/> class.
        /// </summary>
        /// <param name='name'>The name of the user account.</param>
        /// <param name='password'>The password for the user account.</param>
        /// <param name='elevationLevel'>The elevation level of the user account.</param>
        /// <param name='linuxUserConfiguration'>Additional properties used to create a user account on a Linux node.</param>
        /// <param name='windowsUserConfiguration'>The Windows-specific user configuration for the user account.</param>
        public UserAccount(
            string name,
            string password,
            Common.ElevationLevel? elevationLevel = default(Common.ElevationLevel?),
            LinuxUserConfiguration linuxUserConfiguration = default(LinuxUserConfiguration),
            WindowsUserConfiguration windowsUserConfiguration = default(WindowsUserConfiguration))
        {
            this.propertyContainer = new PropertyContainer();
            this.Name = name;
            this.Password = password;
            this.ElevationLevel = elevationLevel;
            this.LinuxUserConfiguration = linuxUserConfiguration;
            this.WindowsUserConfiguration = windowsUserConfiguration;
        }

        internal UserAccount(Models.UserAccount protocolObject)
        {
            this.propertyContainer = new PropertyContainer(protocolObject);
        }

        #endregion Constructors

        #region UserAccount

        /// <summary>
        /// Gets or sets the elevation level of the user account.
        /// </summary>
        /// <remarks>
        /// If omitted, the default is <see cref="Common.ElevationLevel.NonAdmin"/>
        /// </remarks>
        public Common.ElevationLevel? ElevationLevel
        {
            get { return this.propertyContainer.ElevationLevelProperty.Value; }
            set { this.propertyContainer.ElevationLevelProperty.Value = value; }
        }

        /// <summary>
        /// Gets or sets additional properties used to create a user account on a Linux node.
        /// </summary>
        /// <remarks>
        /// This property is ignored if specified on a Windows pool. If not specified, the user is created with the default 
        /// options.
        /// </remarks>
        public LinuxUserConfiguration LinuxUserConfiguration
        {
            get { return this.propertyContainer.LinuxUserConfigurationProperty.Value; }
            set { this.propertyContainer.LinuxUserConfigurationProperty.Value = value; }
        }

        /// <summary>
        /// Gets or sets the name of the user account.
        /// </summary>
        public string Name
        {
            get { return this.propertyContainer.NameProperty.Value; }
            set { this.propertyContainer.NameProperty.Value = value; }
        }

        /// <summary>
        /// Gets or sets the password for the user account.
        /// </summary>
        public string Password
        {
            get { return this.propertyContainer.PasswordProperty.Value; }
            set { this.propertyContainer.PasswordProperty.Value = value; }
        }

        /// <summary>
        /// Gets or sets the Windows-specific user configuration for the user account.
        /// </summary>
        /// <remarks>
        /// This property can only be specified if the user is on a Windows pool. If not specified and on a Windows pool, 
        /// the user is created with the default options.
        /// </remarks>
        public WindowsUserConfiguration WindowsUserConfiguration
        {
            get { return this.propertyContainer.WindowsUserConfigurationProperty.Value; }
            set { this.propertyContainer.WindowsUserConfigurationProperty.Value = value; }
        }

        #endregion // UserAccount

        #region IPropertyMetadata

        bool IModifiable.HasBeenModified
        {
            get { return this.propertyContainer.HasBeenModified; }
        }

        bool IReadOnly.IsReadOnly
        {
            get { return this.propertyContainer.IsReadOnly; }
            set { this.propertyContainer.IsReadOnly = value; }
        }

        #endregion //IPropertyMetadata

        #region Internal/private methods
        /// <summary>
        /// Return a protocol object of the requested type.
        /// </summary>
        /// <returns>The protocol object of the requested type.</returns>
        Models.UserAccount ITransportObjectProvider<Models.UserAccount>.GetTransportObject()
        {
            Models.UserAccount result = new Models.UserAccount()
            {
                ElevationLevel = UtilitiesInternal.MapNullableEnum<Common.ElevationLevel, Models.ElevationLevel>(this.ElevationLevel),
                LinuxUserConfiguration = UtilitiesInternal.CreateObjectWithNullCheck(this.LinuxUserConfiguration, (o) => o.GetTransportObject()),
                Name = this.Name,
                Password = this.Password,
                WindowsUserConfiguration = UtilitiesInternal.CreateObjectWithNullCheck(this.WindowsUserConfiguration, (o) => o.GetTransportObject()),
            };

            return result;
        }

        /// <summary>
        /// Converts a collection of protocol layer objects to object layer collection objects.
        /// </summary>
        internal static IList<UserAccount> ConvertFromProtocolCollection(IEnumerable<Models.UserAccount> protoCollection)
        {
            ConcurrentChangeTrackedModifiableList<UserAccount> converted = UtilitiesInternal.CollectionToThreadSafeCollectionIModifiable(
                items: protoCollection,
                objectCreationFunc: o => new UserAccount(o));

            return converted;
        }

        /// <summary>
        /// Converts a collection of protocol layer objects to object layer collection objects, in a frozen state.
        /// </summary>
        internal static IList<UserAccount> ConvertFromProtocolCollectionAndFreeze(IEnumerable<Models.UserAccount> protoCollection)
        {
            ConcurrentChangeTrackedModifiableList<UserAccount> converted = UtilitiesInternal.CollectionToThreadSafeCollectionIModifiable(
                items: protoCollection,
                objectCreationFunc: o => new UserAccount(o).Freeze());

            converted = UtilitiesInternal.CreateObjectWithNullCheck(converted, o => o.Freeze());

            return converted;
        }

        /// <summary>
        /// Converts a collection of protocol layer objects to object layer collection objects, with each object marked readonly
        /// and returned as a readonly collection.
        /// </summary>
        internal static IReadOnlyList<UserAccount> ConvertFromProtocolCollectionReadOnly(IEnumerable<Models.UserAccount> protoCollection)
        {
            IReadOnlyList<UserAccount> converted =
                UtilitiesInternal.CreateObjectWithNullCheck(
                    UtilitiesInternal.CollectionToNonThreadSafeCollection(
                        items: protoCollection,
                        objectCreationFunc: o => new UserAccount(o).Freeze()), o => o.AsReadOnly());

            return converted;
        }

        #endregion // Internal/private methods
    }
}