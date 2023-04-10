using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    [JsonConverter(typeof(StringEnumConverter), converterParameters: typeof(SnakeCaseNamingStrategy))]
    public enum MediaRole
    {
        Background,
        EditionKeyArt,
        FourByThreeBanner,
        GamehubCoverArt,
        Logo,
        Master,
        PortraitBanner,
        Preview,
        Screenshot,
        BackgroundLayerArt,
        HeroCharacter
    }
}