using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class ActionParameter
    {
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("value")] public string Value { get; private set; }
    }
}