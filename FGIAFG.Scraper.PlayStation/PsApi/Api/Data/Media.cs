using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    [JsonObject]
    public class Media
    {
        [JsonProperty("role")] public MediaRole? Role { get; private set; }
        [JsonProperty("type")] public MediaType? Type { get; private set; }
        [JsonProperty("url")] public string Url { get; private set; }
    }
}