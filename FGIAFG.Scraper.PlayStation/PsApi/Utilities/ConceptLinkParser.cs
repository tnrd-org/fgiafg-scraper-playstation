using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Data;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Utilities
{
    public class ConceptLinkParser : ILinkParser
    {
        private readonly IConceptApi conceptApi;

        public ConceptLinkParser(IConceptApi conceptApi)
        {
            this.conceptApi = conceptApi;
        }

        /// <inheritdoc />
        public bool CanParse(string url)
        {
            return url.StartsWith("https://store.playstation.com/concept");
        }

        /// <inheritdoc />
        public async Task<Result<Concept>> Parse(string url, CancellationToken cancellationToken)
        {
            string trimmed = url.Trim('/');
            string conceptId = trimmed[trimmed.LastIndexOf('/')..].Trim('/');
            return await conceptApi.RetrieveForCtasWithPrice(conceptId, cancellationToken);
        }
    }
}