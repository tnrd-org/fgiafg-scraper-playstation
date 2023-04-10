using AngleSharp;
using AngleSharp.Dom;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Data;
using FluentResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FGIAFG.Scraper.PlayStation.PsApi.Utilities
{
    public class DirectLinkParser : ILinkParser
    {
        private readonly HttpClient httpClient;
        private readonly IConceptApi conceptApi;

        public DirectLinkParser(HttpClient httpClient, IConceptApi conceptApi)
        {
            this.httpClient = httpClient;
            this.conceptApi = conceptApi;
        }

        /// <inheritdoc />
        public bool CanParse(string url)
        {
            return url.StartsWith("https://www.playstation.com/en-us/games");
        }

        /// <inheritdoc />
        public async Task<Result<Concept>> Parse(string url, CancellationToken cancellationToken)
        {
            string content = await GetContentFromUrl(url);
            string conceptId = await GetConceptIdFromContent(url, content);
            return await conceptApi.RetrieveForCtasWithPrice(conceptId, cancellationToken);
        }

        private async Task<string> GetContentFromUrl(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        private async Task<string> GetConceptIdFromContent(string url, string content)
        {
            BrowsingContext context = new(Configuration.Default.WithDefaultLoader());
            IDocument document = await context.OpenAsync(x => x.Address(url).Content(content));
            string productInfo = document.Body.GetAttribute("data-product-info");
            JObject jObject = JsonConvert.DeserializeObject<JObject>(productInfo);
            return jObject["conceptId"].Value<string>();
        }
    }
}
