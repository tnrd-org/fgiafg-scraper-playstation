using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class Action
    {
        [JsonProperty("param")] public IReadOnlyList<ActionParameter> Parameters { get; private set; }
        [JsonProperty("type")] public ActionType? Type { get; private set; }
    }
}