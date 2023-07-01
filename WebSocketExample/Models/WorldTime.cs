using System.Text.Json.Serialization;

namespace WebSocketExample.Models;

public class WorldTime
{
    [JsonPropertyName("abbreviation")]
    public string? Abbreviation { get; set; }

    [JsonPropertyName("client_ip")]
    public string? ClientIp { get; set; }

    [JsonPropertyName("datetime")]
    public DateTime Datetime { get; set; }

    [JsonPropertyName("day_of_week")]
    public int DayOfWeek { get; set; }

    [JsonPropertyName("day_of_year")]
    public int DayOfYear { get; set; }

    [JsonPropertyName("dst")]
    public bool Dst { get; set; }

    [JsonPropertyName("dst_from")]
    public string? DstFrom { get; set; }

    [JsonPropertyName("dst_offset")]
    public int DstOffset { get; set; }

    [JsonPropertyName("dst_until")]
    public string? DstUntil { get; set; }

    [JsonPropertyName("raw_offset")]
    public int RawOffset { get; set; }

    [JsonPropertyName("timezone")]
    public string? Timezone { get; set; }

    [JsonPropertyName("unixtime")]
    public int Unixtime { get; set; }

    [JsonPropertyName("utc_datetime")]
    public DateTime UtcDatetime { get; set; }

    [JsonPropertyName("utc_offset")]
    public string? UtcOffset { get; set; }

    [JsonPropertyName("week_number")]
    public int WeekNumber { get; set; }
}