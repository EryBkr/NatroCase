namespace Domain.Dtos;

using System.Text.Json.Serialization;

public record Entity
{
    [JsonPropertyName("objectClassName")]
    public string ObjectClassName { get; init; }

    [JsonPropertyName("handle")]
    public string Handle { get; init; }

    [JsonPropertyName("roles")]
    public List<string> Roles { get; init; }

    [JsonPropertyName("publicIds")]
    public List<PublicId> PublicIds { get; init; }

    [JsonPropertyName("vcardArray")]
    public List<object> VCardArray { get; init; }

    [JsonPropertyName("entities")]
    public List<Entity> Entities { get; init; }
}

public record Event
{
    [JsonPropertyName("eventAction")]
    public string EventAction { get; init; }

    [JsonPropertyName("eventDate")]
    public DateTime EventDate { get; init; }
}

public record Link
{
    [JsonPropertyName("value")]
    public string Value { get; init; }

    [JsonPropertyName("rel")]
    public string Rel { get; init; }

    [JsonPropertyName("href")]
    public string Href { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; }
}

public record Nameserver
{
    [JsonPropertyName("objectClassName")]
    public string ObjectClassName { get; init; }

    [JsonPropertyName("ldhName")]
    public string LdhName { get; init; }

    [JsonPropertyName("status")]
    public List<string> Status { get; init; }
}

public record Notice
{
    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("description")]
    public List<string> Description { get; init; }

    [JsonPropertyName("links")]
    public List<Link> Links { get; init; }
}

public record PublicId
{
    [JsonPropertyName("type")]
    public string Type { get; init; }

    [JsonPropertyName("identifier")]
    public string Identifier { get; init; }
}

public record RdapResponse
{
    [JsonPropertyName("objectClassName")]
    public string ObjectClassName { get; init; }

    [JsonPropertyName("handle")]
    public string? Handle { get; init; }

    [JsonPropertyName("ldhName")]
    public string LdhName { get; init; }

    [JsonPropertyName("status")]
    public List<string> Status { get; init; }

    [JsonPropertyName("events")]
    public List<Event> Events { get; init; }

    [JsonPropertyName("secureDNS")]
    public SecureDNS SecureDNS { get; init; }

    [JsonPropertyName("links")]
    public List<Link> Links { get; init; }

    [JsonPropertyName("nameservers")]
    public List<Nameserver> Nameservers { get; init; }

    [JsonPropertyName("rdapConformance")]
    public List<string> RdapConformance { get; init; }

    [JsonPropertyName("notices")]
    public List<Notice> Notices { get; init; }

    [JsonPropertyName("entities")]
    public List<Entity> Entities { get; init; }
}

public record SecureDNS
{
    [JsonPropertyName("delegationSigned")]
    public bool DelegationSigned { get; init; }
}









