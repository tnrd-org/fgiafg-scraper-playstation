using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Product.Parameters
{
    public class ProductIdParameters : IParameters
    {
        [JsonProperty("productId")] public string ProductId { get; set; }

        public ProductIdParameters(string productId)
        {
            ProductId = productId;
        }

        /// <inheritdoc />
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}