using System.Text.Json.Serialization;

namespace getquoteslistforu;

public class Quote
{
    public object? _id { get; set; }

    public int? quoteID { get; set; }

    public string? author { get; set; }

    public string? quotation { get; set; }
}
