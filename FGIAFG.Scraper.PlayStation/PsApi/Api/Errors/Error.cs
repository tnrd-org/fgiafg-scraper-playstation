using System.Text.Json.Serialization;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Errors
{
    [Serializable]
    public sealed class Error
    {
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("error")] public InnerError InnerError { get; set; }
        [JsonPropertyName("statusCode")] public int StatusCode { get; set; }
        [JsonPropertyName("errorCode")] public int ErrorCode { get; set; }

        [JsonPropertyName("humanReadableCode")]
        public string HumanReadableCode { get; set; }

        [JsonPropertyName("humanReadableValidationErrors")]
        public IReadOnlyList<string> HumanReadableValidationErrors { get; set; }

        [JsonPropertyName("apiName")] public string ApiName { get; set; }
    }
}