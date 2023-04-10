using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions
{
    public interface IProductApi
    {
        Task<Result<Data.Product>> RetrieveForCtasWithPrice(string productId, CancellationToken cancellationToken);
    }
}