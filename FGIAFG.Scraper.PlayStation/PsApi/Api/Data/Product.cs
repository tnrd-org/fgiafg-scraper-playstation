using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    [JsonObject]
    public class Product
    {
        [JsonProperty("edition")] public Edition Edition { get; private set; }
        [JsonProperty("media")] public IReadOnlyList<Media> Media { get; private set; }
        [JsonProperty("platforms")] public IReadOnlyList<Platform> Platforms { get; private set; }
        [JsonProperty("topCategory")] public Category? TopCategory { get; private set; }
        [JsonProperty("concept")] public Concept Concept { get; private set; }
        [JsonProperty("id")] public string Id { get; private set; }
        [JsonProperty("isInWishlist")] public bool? IsInWishlist { get; private set; }
        [JsonProperty("isWishlistable")] public bool? IsWishlistable { get; private set; }
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("skus")] public IReadOnlyList<Sku> Skus { get; private set; }
        [JsonProperty("webctas")]public IReadOnlyList<GameCta> WebCtas { get; private set; }
    }
}