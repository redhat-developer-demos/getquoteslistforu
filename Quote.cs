using System.Text.Json.Serialization;

namespace getquoteslistforu;

public class Quote
{
    [JsonPropertyName("_id")]
    public object? mongo_id { get; set; }

    [JsonPropertyName("id")]
    public int? id { get; set; }

    public string? author { get; set; }

    public string? quotation { get; set; }
}
