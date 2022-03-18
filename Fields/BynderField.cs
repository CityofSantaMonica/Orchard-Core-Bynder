using Newtonsoft.Json;
using OrchardCore.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSM.Bynder.Fields;

public class BynderResourceDerivative
{
    [JsonProperty("name")]
    public string Name { get; set; }
    public Uri Uri { get; set; }

    // Only necessary for JSON de/serialization.
    [JsonProperty("url")]
    public string Url { get => Uri?.ToString(); set => Uri = new Uri(value); }
}

public class BynderResource
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("hashId")]
    public string HashId { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("derivatives")]
    public List<BynderResourceDerivative> Derivatives { get; } = new();

    public BynderResourceDerivative GetDerivative(string name) =>
        Derivatives.FirstOrDefault(derivative => string.Equals(derivative.Name, name, StringComparison.Ordinal));
}

public class BynderField : ContentField
{
    public List<BynderResource> Resources { get; } = new();
}
