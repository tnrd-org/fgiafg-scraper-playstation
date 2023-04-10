using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Data;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.Scraping;

internal class PlayStationScraper
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ILogger<PlayStationScraper> logger;
    private readonly IPsPlusApi psPlusApi;

    public PlayStationScraper(
        IHttpClientFactory httpClientFactory,
        ILogger<PlayStationScraper> logger,
        IPsPlusApi psPlusApi
    )
    {
        this.httpClientFactory = httpClientFactory;
        this.logger = logger;
        this.psPlusApi = psPlusApi;
    }

    public async Task<Result<IEnumerable<FreeGame>>> Scrape(CancellationToken cancellationToken)
    {
        List<FreeGame> freeGames = new();
        Result<IReadOnlyList<Concept>> monthlyGamesResult = await psPlusApi.GetMonthlyGames(cancellationToken);

        if (monthlyGamesResult.IsFailed)
        {
            return monthlyGamesResult.ToResult();
        }

        foreach (Concept concept in monthlyGamesResult.Value)
        {
            Media? image = concept.Media
                .Where(x => x.Type == MediaType.Image)
                .FirstOrDefault(x => x.Role == MediaRole.Master);

            DateTime? endDateTime = GetEndDateTime(concept);
            FreeGame freeGame = new FreeGame(concept.Name,
                image?.Url ?? string.Empty,
                $"https://store.playstation.com/en-us/concept/{concept.Id}",
                null,
                endDateTime);

            freeGames.Add(freeGame);
        }

        return Result.Ok(freeGames.AsEnumerable());
    }

    private DateTime? GetEndDateTime(Concept concept)
    {
        foreach (Product product in concept.Products)
        {
            foreach (GameCta cta in product.WebCtas)
            {
                if (cta.Type != ActionType.UpsellPsPlusFree)
                    continue;

                return cta.Price.EndTime;
            }
        }

        return null;
    }
}
