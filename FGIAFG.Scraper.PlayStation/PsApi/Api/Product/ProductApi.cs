using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Data.Wrappers;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Product.Parameters;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Product
{
    internal class ProductApi : IProductApi
    {
        private readonly PsApiHttpClient psApiHttpClient;

        public ProductApi(PsApiHttpClient psApiHttpClient)
        {
            this.psApiHttpClient = psApiHttpClient;
        }

        /// <inheritdoc />
        public async Task<Result<Data.Product>> RetrieveForCtasWithPrice(string productId,
            CancellationToken cancellationToken)
        {
            Result<ProductWrapper> result = await psApiHttpClient.GetAsync<ProductWrapper>(
                "productRetrieveForCtasWithPrice",
                new ProductIdParameters(productId),
                "dd61c9db18f39d1459b0b4927a58335125ca801c584ced5e138261075da230b2",
                cancellationToken);

            return result.IsSuccess
                ? Result.Ok(result.Value.data.productRetrieve)
                : result.ToResult();
        }
    }
}