using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Concept.Parameters;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Data.Wrappers;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Concept
{
    internal class ConceptApi : IConceptApi
    {
        private readonly PsApiHttpClient psApiHttpClient;

        public ConceptApi(PsApiHttpClient psApiHttpClient)
        {
            this.psApiHttpClient = psApiHttpClient;
        }

        /// <inheritdoc />
        public async Task<Result<Data.Concept>> RetrieveForCtasWithPrice(string conceptId,
            CancellationToken cancellationToken)
        {
            Result<ConceptWrapper> result = await psApiHttpClient.GetAsync<ConceptWrapper>(
                "conceptRetrieveForCtasWithPrice",
                new ConceptIdParameters(conceptId),
                "68e483c8c56ded35047fc3015aa528c6191bf50bce2aae4f190120a1be1c8ba3",
                cancellationToken);

            return result.IsSuccess
                ? Result.Ok(result.Value.data.conceptRetrieve)
                : result.ToResult();
        }

        /// <inheritdoc />
        public async Task<Result<Data.Concept>> RetrieveForUpsellWithCtas(string conceptId,
            CancellationToken cancellationToken)
        {
            Result<ConceptWrapper> result = await psApiHttpClient.GetAsync<ConceptWrapper>(
                "conceptRetrieveForUpsellWithCtas",
                new ConceptIdParameters(conceptId),
                "4460d00afbeeb676c0d6ef3365b73d1c4f122378b175d6fda13b8ee07ca1d0a2",
                cancellationToken);

            return result.IsSuccess
                ? Result.Ok(result.Value.data.conceptRetrieve)
                : result.ToResult();
        }
    }
}