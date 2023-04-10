using FGIAFG.Scraper.PlayStation.PsApi.Newtonsoft;
using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    public class Price
    {
        [JsonProperty("applicability")] public Applicability? Applicability { get; private set; }
        [JsonProperty("basePrice")] public string BasePrice { get; private set; }
        [JsonProperty("basePriceValue")] public long? BasePriceValue { get; private set; }
        [JsonProperty("campaignId")] public string CampaignId { get; private set; }

        // TODO: Should this be an enum?
        [JsonProperty("currencyCode")] public string CurrencyCode { get; private set; }
        [JsonProperty("discountText")] public string DiscountText { get; private set; }
        [JsonProperty("discountedPrice")] public string DiscountedPrice { get; private set; }
        [JsonProperty("discountedValue")] public long? DiscountedValue { get; private set; }

        [JsonProperty("endTime")]
        [JsonConverter(typeof(MillisecondEpochConverter))]
        public DateTime? EndTime { get; private set; }

        [JsonProperty("isExclusive")] public bool? IsExclusive { get; private set; }
        [JsonProperty("isTiedToSubscription")] public bool? IsTiedToSubscription { get; private set; }

        // TODO: Figure out type
        [JsonProperty("qualifications")] public IReadOnlyList<object> Qualifications { get; private set; }
        [JsonProperty("rewardId")] public string RewardId { get; private set; }

        // TODO: Figure out type
        [JsonProperty("serviceBranding")] public IReadOnlyList<object> ServiceBranding { get; private set; }
        [JsonProperty("upsellText")] public string UpsellText { get; private set; }
    }
}