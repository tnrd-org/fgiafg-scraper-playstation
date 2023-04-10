using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Concept;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Product;
using FGIAFG.Scraper.PlayStation.PsApi.Api.PsPlus;
using FGIAFG.Scraper.PlayStation.PsApi.Utilities;
using Microsoft.Extensions.Options;
using Polly;

namespace FGIAFG.Scraper.PlayStation.PsApi
{
    public static class Extensions
    {
        public static IServiceCollection UsePsApi(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient("PsPlus",
                    (provider, client) =>
                    {
                        client.BaseAddress = new Uri("https://www.playstation.com/en-us/ps-plus/");
                    })
                .AddTransientHttpErrorPolicy(builder => builder
                    .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(1)));

            serviceCollection.AddHttpClient("PsApi", (provider, client) =>
                {
                    client.BaseAddress = new Uri("https://web.np.playstation.com/api/graphql/v1/op");

                    IOptions<PsApiOptions> psApiOptions = provider.GetRequiredService<IOptions<PsApiOptions>>();
                    client.DefaultRequestHeaders.Add("X-Psn-Store-Locale-Override", psApiOptions.Value.Region);
                })
                .AddTransientHttpErrorPolicy(builder => builder
                    .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(1)));

            return serviceCollection
                .AddTransient<PsApiHttpClient>()
                .AddSingleton<IProductApi, ProductApi>()
                .AddSingleton<IConceptApi, ConceptApi>()
                .AddSingleton<IPsPlusApi, PsPlusApi>()
                .AddTransient<ILinkParser, DirectLinkParser>()
                .AddTransient<ILinkParser, ConceptLinkParser>();
        }
    }
}
