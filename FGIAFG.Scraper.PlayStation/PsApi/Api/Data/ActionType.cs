using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data
{
    [JsonConverter(typeof(StringEnumConverter), converterParameters: typeof(SnakeCaseNamingStrategy))]
    public enum ActionType
    {
        AddToCart,
        UpsellPsPlusFree,
        BackgroundPurchaseAndDownload,
        DownloadTrial,
        UpsellEaAccessDiscount,
        UpsellPsPlusDiscount,
        BuyNow,
        Preorder,
        Download,
        UpsellEaAccessFree,
        DownloadDemo
    }
}