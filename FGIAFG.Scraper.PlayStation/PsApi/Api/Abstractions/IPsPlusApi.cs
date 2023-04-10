using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions
{
    public interface IPsPlusApi
    {
        Task<Result<IReadOnlyList<Data.Concept>>> GetMonthlyGames(CancellationToken cancellationToken);
    }
}