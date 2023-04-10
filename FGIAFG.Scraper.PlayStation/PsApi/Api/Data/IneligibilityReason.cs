using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class IneligibilityReason
    {
        [JsonProperty("names")] public IReadOnlyList<string> Names { get; private set; }
        [JsonProperty("type")] public string Type { get; private set; }
    }
}