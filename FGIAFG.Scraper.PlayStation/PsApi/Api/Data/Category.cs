using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    [JsonConverter(typeof(StringEnumConverter), converterParameters: typeof(SnakeCaseNamingStrategy))]
    public enum Category
    {
        Game,
        Demo
    }
}