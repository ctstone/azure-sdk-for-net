// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.Search.Models
{
    public partial class SearchServiceCounters : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (DocumentCounter != null)
            {
                writer.WritePropertyName("documentCount");
                writer.WriteObjectValue(DocumentCounter);
            }
            if (IndexCounter != null)
            {
                writer.WritePropertyName("indexesCount");
                writer.WriteObjectValue(IndexCounter);
            }
            if (IndexerCounter != null)
            {
                writer.WritePropertyName("indexersCount");
                writer.WriteObjectValue(IndexerCounter);
            }
            if (DataSourceCounter != null)
            {
                writer.WritePropertyName("dataSourcesCount");
                writer.WriteObjectValue(DataSourceCounter);
            }
            if (StorageSizeCounter != null)
            {
                writer.WritePropertyName("storageSize");
                writer.WriteObjectValue(StorageSizeCounter);
            }
            if (SynonymMapCounter != null)
            {
                writer.WritePropertyName("synonymMaps");
                writer.WriteObjectValue(SynonymMapCounter);
            }
            writer.WriteEndObject();
        }
        internal static SearchServiceCounters DeserializeSearchServiceCounters(JsonElement element)
        {
            SearchServiceCounters result = new SearchServiceCounters();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("documentCount"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.DocumentCounter = SearchResourceCounter.DeserializeSearchResourceCounter(property.Value);
                    continue;
                }
                if (property.NameEquals("indexesCount"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.IndexCounter = SearchResourceCounter.DeserializeSearchResourceCounter(property.Value);
                    continue;
                }
                if (property.NameEquals("indexersCount"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.IndexerCounter = SearchResourceCounter.DeserializeSearchResourceCounter(property.Value);
                    continue;
                }
                if (property.NameEquals("dataSourcesCount"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.DataSourceCounter = SearchResourceCounter.DeserializeSearchResourceCounter(property.Value);
                    continue;
                }
                if (property.NameEquals("storageSize"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.StorageSizeCounter = SearchResourceCounter.DeserializeSearchResourceCounter(property.Value);
                    continue;
                }
                if (property.NameEquals("synonymMaps"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.SynonymMapCounter = SearchResourceCounter.DeserializeSearchResourceCounter(property.Value);
                    continue;
                }
            }
            return result;
        }
    }
}
