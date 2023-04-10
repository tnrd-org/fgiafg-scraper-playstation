using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class ReleaseDate
    {
        [JsonProperty("type")] public string Type { get; private set; }
        [JsonProperty("value")] public DateTimeOffset? Value { get; private set; }
    }
}