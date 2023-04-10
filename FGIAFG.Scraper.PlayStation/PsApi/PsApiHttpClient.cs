using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using FGIAFG.Scraper.PlayStation.PsApi.Api.Errors;
using FGIAFG.Scraper.PlayStation.PsApi.Results;
using FluentResults;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FGIAFG.Scraper.PlayStation.PsApi
{
    internal class PsApiHttpClient
    {
        private const string BASE_URL = "https://web.np.playstation.com/api/graphql/v1/op";

        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<PsApiHttpClient> logger;
        private readonly PsApiOptions options;

        public PsApiHttpClient(IHttpClientFactory httpClientFactory, ILogger<PsApiHttpClient> logger,
            IOptions<PsApiOptions> options)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
            this.options = options.Value;
        }

        public async Task<Result<string>> GetAsync(string operation, IParameters parameters, string hash,
            CancellationToken cancellationToken)
        {
            string url = BuildUrl(operation, parameters, hash);

            HttpClient httpClient = httpClientFactory.CreateClient("PsApi");

            using (HttpResponseMessage response = await httpClient.GetAsync(url, cancellationToken))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return Result.Fail(new HttpResultError(response.StatusCode, response.ReasonPhrase));
                }

                string content = await response.Content.ReadAsStringAsync(cancellationToken);
                ErrorWrapper errorWrapper = JsonSerializer.Deserialize<ErrorWrapper>(content)!;

                if (errorWrapper.Errors == null)
                    return Result.Ok(content);

                return Result.Fail(new PsApiError(errorWrapper.Errors));
            }
        }

        public async Task<Result<TData>> GetAsync<TData>(string operation, IParameters parameters, string hash,
            CancellationToken cancellationToken)
        {
            Result<string> result = await GetAsync(operation, parameters, hash, cancellationToken);

            if (result.IsFailed)
                return result.ToResult();

            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Include
            };

            try
            {
                TData data = JsonConvert.DeserializeObject<TData>(result.Value, settings);
                return Result.Ok(data);
            }
            catch (Exception e)
            {
                return Result.Fail(new ExceptionalError(e));
            }
        }

        private string BuildUrl(string operation, IParameters parameters, string hash)
        {
            return $"?operationName={operation}&variables={parameters.ToJson()}&extensions={BuildExtensions(hash)}";
        }

        private string BuildExtensions(string hash)
        {
            return "{\"persistedQuery\":{\"version\":1,\"sha256Hash\":\"$hash\"}}".Replace("$hash", hash);
        }
    }
}