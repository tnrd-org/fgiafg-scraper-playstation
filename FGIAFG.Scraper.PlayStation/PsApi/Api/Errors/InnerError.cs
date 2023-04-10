using System.Text.Json.Serialization;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Errors
{
    [Serializable]
    public sealed class InnerError
    {
        [JsonPropertyName("referenceId")] public string ReferenceId { get; set; }
        [JsonPropertyName("code")] public int Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("reason")] public string Reason { get; set; }
        [JsonPropertyName("source")] public string Source { get; set; }
    }
}