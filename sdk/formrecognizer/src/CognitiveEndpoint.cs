// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Azure.AI.FormRecognizer
{
    /// <summary>
    /// A Cognitive Services endpoint, typically in the form of `https://{region}.api.cognitive.microsoft.com/`.
    /// </summary>
    public readonly struct CognitiveEndpoint : IEquatable<CognitiveEndpoint>
    {
        private const string CognitiveHost = "api.cognitive.microsoft.com";
        private readonly Uri _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="CognitiveEndpoint"/> struct.
        /// </summary>
        /// <param name="uri"></param>
        public CognitiveEndpoint(Uri uri)
        {
            _value = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        /// <summary>
        /// https://australiaeast.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint AustraliaEast => GetUriForRegion("australiaeast");

        /// <summary>
        /// https://canadacentral.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint CanadaCentral => GetUriForRegion("canadacentral");

        /// <summary>
        /// https://centralus.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint CentralUnitedStates => GetUriForRegion("centralus");

        /// <summary>
        /// https://centraluseuap.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint CentralUnitedStatesCanary => GetUriForRegion("centraluseuap");

        /// <summary>
        /// https://eastasia.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint EastAsia => GetUriForRegion("eastasia");

        /// <summary>
        /// https://eastus.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint EastUnitedStates => GetUriForRegion("eastus");

        /// <summary>
        /// https://eastus2.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint EastUnitedStates2 => GetUriForRegion("eastus2");

        /// <summary>
        /// https://northeurope.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint NorthEurope => GetUriForRegion("northeurope");

        /// <summary>
        /// https://southcentralus.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint SouthCentralUnitedStates => GetUriForRegion("southcentralus");

        /// <summary>
        /// https://southeastasia.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint SoutheastAsia => GetUriForRegion("southeastasia");

        /// <summary>
        /// https://uksouth.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint UnitedKingdomSouth => GetUriForRegion("uksouth");

        /// <summary>
        /// https://westeurope.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint WestEurope => GetUriForRegion("westeurope");

        /// <summary>
        /// https://westus2.api.cognitive.microsoft.com/
        /// </summary>
        public static CognitiveEndpoint WestUnitedStates2 => GetUriForRegion("westus2");

        /// <inheritdoc />
        public bool Equals(CognitiveEndpoint other) => _value.OriginalString.Equals(((Uri)other).OriginalString, StringComparison.Ordinal);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is CognitiveEndpoint other && Equals(other);

        /// <inheritdoc/>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;

        /// <inheritdoc/>
        public override string ToString() => _value.ToString();

        /// <summary>
        /// Converts a <see cref="Uri" /> to a <see cref="CognitiveEndpoint"/>.
        /// </summary>
        /// <param name="value">The Uri value to convert.</param>
        public static implicit operator CognitiveEndpoint(Uri value) => new CognitiveEndpoint(value);

        /// <summary>
        /// Converts a <see cref="CognitiveEndpoint"/> to a <see cref="Uri" />.
        /// </summary>
        /// <param name="endpoint">The endpoint to convert.</param>
        public static implicit operator Uri(CognitiveEndpoint endpoint) => endpoint._value;

        /// <summary>
        /// Determines if two <see cref="CognitiveEndpoint"/> values are the same.
        /// </summary>
        /// <param name="left">The first <see cref="CognitiveEndpoint"/> to compare.</param>
        /// <param name="right">The first <see cref="CognitiveEndpoint"/> to compare.</param>
        public static bool operator ==(CognitiveEndpoint left, CognitiveEndpoint right) => left.Equals(right);

        /// <summary>
        /// Determines if two <see cref="CognitiveEndpoint"/> values are different.
        /// </summary>
        /// <param name="left">The first <see cref="CognitiveEndpoint"/> to compare.</param>
        /// <param name="right">The first <see cref="CognitiveEndpoint"/> to compare.</param>
        public static bool operator !=(CognitiveEndpoint left, CognitiveEndpoint right) => !left.Equals(right);

        private static Uri GetUriForRegion(string region) => new Uri($"https://{region}.{CognitiveHost}/");
    }
}