using System.Text.Json.Serialization;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Errors
{
    [Serializable]
    public sealed class ErrorWrapper
    {
        [JsonPropertyName("errors")] public IReadOnlyList<Error> Errors { get; set; }
    }
}