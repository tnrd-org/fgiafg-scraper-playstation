using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class CtaMeta
    {
        [JsonProperty("exclusive")] public bool? Exclusive { get; private set; }
        [JsonProperty("playabilityDate")] public DateTimeOffset? PlayabilityDate { get; private set; }
        [JsonProperty("upSellService")] public string UpSellService { get; private set; }
    }
}