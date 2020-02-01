// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.AI.FormRecognizer.Training
{
    /// <summary>
    /// </summary>
    public class FormCluster
    {
        /// <summary>
        /// Cluster Id
        /// </summary>
        public int FormClusterId { get; internal set; }

        /// <summary>
        /// Names of the fields extracted from forms in this cluster.
        /// </summary>
        public IEnumerable<string> FieldNames { get; internal set; }
    }
}
