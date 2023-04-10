using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class GameCta
    {
        [JsonProperty("action")] public Action Action { get; private set; }
        [JsonProperty("hasLinkedConsole")] public bool? HasLinkedConsole { get; private set; }
        [JsonProperty("meta")] public CtaMeta Meta { get; private set; }
        [JsonProperty("type")] public ActionType? Type { get; private set; }
        [JsonProperty("price")] public Price Price { get; private set; }
    }
}