using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions
{
    public interface IConceptApi
    {
        Task<Result<Data.Concept>> RetrieveForCtasWithPrice(string conceptId, CancellationToken cancellationToken);
        Task<Result<Data.Concept>> RetrieveForUpsellWithCtas(string conceptId, CancellationToken cancellationToken);
    }
}