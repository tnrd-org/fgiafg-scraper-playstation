using System.Net;
using FluentResults;

namespace FGIAFG.Scraper.PlayStation.PsApi.Results
{
    public sealed class HttpResultError : Error
    {
        public HttpStatusCode StatusCode { get; }

        public HttpResultError(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}