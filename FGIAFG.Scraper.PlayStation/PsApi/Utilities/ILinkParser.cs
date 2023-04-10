using FGIAFG.Scraper.PlayStation.PsApi.Api.Data;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Utilities
{
    public interface ILinkParser
    {
        bool CanParse(string url);
        Task<Result<Concept>> Parse(string url, CancellationToken cancellationToken);
    }
}