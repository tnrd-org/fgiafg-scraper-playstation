using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class Sku
    {
        [JsonProperty("id")] public string Id { get; private set; }
        [JsonProperty("name")] public string Name { get; private set; }
    }
}