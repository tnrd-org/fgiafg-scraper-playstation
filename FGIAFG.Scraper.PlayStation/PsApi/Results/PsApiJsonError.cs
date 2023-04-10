using FGIAFG.Scraper.PlayStation.PsApi.Api.Errors;

namespace FGIAFG.Scraper.PlayStation.PsApi.Results
{
    public sealed class PsApiError : FluentResults.Error
    {
        public IReadOnlyList<Error> Errors { get; }

        public PsApiError(IReadOnlyList<Error> errors) : base("One or more API errors have occured")
        {
            Errors = errors;
        }
    }
}