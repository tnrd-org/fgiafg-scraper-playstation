using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    [JsonObject]
    public class Concept : IEquatable<Concept>
    {
        [JsonProperty("defaultProduct")] public Product DefaultProduct { get; private set; }
        [JsonProperty("id")] public string Id { get; private set; }
        [JsonProperty("media")] public List<Media> Media { get; private set; }
        [JsonProperty("name")] public string Name { get; private set; }
        [JsonProperty("isInWishlist")] public bool? IsInWishlist { get; private set; }
        [JsonProperty("isWishlistable")] public bool? IsWishlistable { get; private set; }
        [JsonProperty("products")] public List<Product> Products { get; private set; }
        [JsonProperty("releaseDate")] public ReleaseDate ReleaseDate { get; private set; }

        /// <inheritdoc />
        public bool Equals(Concept other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Concept)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}