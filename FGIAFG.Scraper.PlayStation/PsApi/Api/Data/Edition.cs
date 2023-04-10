using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class Edition
    {
        [JsonProperty("features")] public IReadOnlyList<string> Features { get; private set; }
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("ordering")] public int? Ordering { get; private set; }
        [JsonProperty("type")] public EditionType? Type { get; private set; }
    }
}