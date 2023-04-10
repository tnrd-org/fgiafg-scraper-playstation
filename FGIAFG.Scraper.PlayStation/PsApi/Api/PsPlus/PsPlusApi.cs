using System.Net;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Data;
using FGIAFG.Scraper.PlayStation.PsApi.Results;
using FGIAFG.Scraper.PlayStation.PsApi.Utilities;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.PsPlus
{
    internal class PsPlusApi : IPsPlusApi
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IEnumerable<ILinkParser> linkParsers;
        private readonly IProductApi productApi;
        private readonly IConceptApi conceptApi;

        public PsPlusApi(IHttpClientFactory httpClientFactory, IEnumerable<ILinkParser> linkParsers,
            IProductApi productApi,
            IConceptApi conceptApi)
        {
            this.httpClientFactory = httpClientFactory;
            this.linkParsers = linkParsers;
            this.productApi = productApi;
            this.conceptApi = conceptApi;
        }

        /// <inheritdoc />
        public async Task<Result<IReadOnlyList<Data.Concept>>> GetMonthlyGames(CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<IHtmlAnchorElement> anchors = await GetAnchors(cancellationToken);
                return await ParseAnchors(anchors, cancellationToken);
            }
            catch (HttpRequestException e)
            {
                return Result.Fail(new HttpResultError(e.StatusCode ?? HttpStatusCode.Ambiguous, e.Message));
            }
        }

        private async Task<IEnumerable<IHtmlAnchorElement>> GetAnchors(CancellationToken cancellationToken)
        {
            const string url = "https://www.playstation.com/en-us/ps-plus/this-month-on-ps-plus/";

            HttpClient httpClient = httpClientFactory.CreateClient("PsPlus");
            HttpResponseMessage response = await httpClient.GetAsync("this-month-on-ps-plus/", cancellationToken);
            response.EnsureSuccessStatusCode();
            string html = await response.Content.ReadAsStringAsync(cancellationToken);

            IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            IDocument document = await context.OpenAsync(x => x.Address(url).Content(html), cancel: cancellationToken);

            return document.Body.QuerySelectorAll<IHtmlAnchorElement>(".section .button a").ToList();
        }

        private async Task<Result<IReadOnlyList<Data.Concept>>> ParseAnchors(IEnumerable<IHtmlAnchorElement> anchors,
            CancellationToken cancellationToken)
        {
            List<IReason> reasons = new();
            List<Data.Concept> concepts = new();

            foreach (IHtmlAnchorElement anchor in anchors)
            {
                Result<List<Data.Concept>> parsedConceptsResult = await ParseAnchor(anchor, cancellationToken);
                reasons.AddRange(parsedConceptsResult.Reasons);
                
                if (parsedConceptsResult.ValueOrDefault != null)
                {
                    concepts.AddRange(parsedConceptsResult.ValueOrDefault);
                }
            }

            return Result.Ok((IReadOnlyList<Data.Concept>)concepts)
                .WithReasons(reasons);
        }

        private async Task<Result<List<Data.Concept>>> ParseAnchor(IHtmlAnchorElement anchor,
            CancellationToken cancellationToken)
        {
            ILinkParser parser = linkParsers.FirstOrDefault(x => x.CanParse(anchor.Href));
            if (parser == null)
                return Result.Ok(new List<Data.Concept>());

            Result<Data.Concept> result = await parser.Parse(anchor.Href, cancellationToken);
            if (result.IsFailed)
                return result.ToResult();

            return await ParseProducts(result.Value.Products, cancellationToken);
        }

        private async Task<Result<List<Data.Concept>>> ParseProducts(IEnumerable<Data.Product> products,
            CancellationToken cancellationToken)
        {
            List<IReason> reasons = new List<IReason>();
            List<Data.Concept> concepts = new();
            foreach (Data.Product product in products)
            {
                Result<Data.Concept> conceptResult = await ParseProduct(product, cancellationToken);
                if (conceptResult.IsFailed)
                {
                    reasons.AddRange(conceptResult.Reasons);
                }
                else if (conceptResult.ValueOrDefault != null)
                {
                    concepts.Add(conceptResult.Value);
                }
            }

            return Result.Ok(concepts.Distinct().ToList())
                .WithReasons(reasons);
        }

        private async Task<Result<Data.Concept>> ParseProduct(Data.Product product, CancellationToken cancellationToken)
        {
            Result<Data.Product> result = await productApi.RetrieveForCtasWithPrice(product.Id!, cancellationToken);

            if (result.IsFailed)
            {
                return result.ToResult();
            }

            List<GameCta> upsells = result.Value.WebCtas
                .Where(x => x.Type == ActionType.UpsellPsPlusFree)
                .ToList();

            if (upsells.Count == 0)
                return Result.Ok();

            if (upsells.Count > 1)
                return Result.Fail("Unexpected amount of PsPlusFree upsells");

            GameCta upsell = upsells.First();

            if (upsell.Price?.EndTime == null)
                return Result.Ok();

            return await conceptApi.RetrieveForUpsellWithCtas(result.Value.Concept.Id, cancellationToken);
        }
    }
}
