using FGIAFG.Scraper.PlayStation.PsApi.Api.Abstractions;
using Newtonsoft.Json;

namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Concept.Parameters
{
    public class ConceptIdParameters : IParameters
    {
        [JsonProperty("conceptId")] public string ConceptId { get; set; }

        public ConceptIdParameters(string conceptId)
        {
            ConceptId = conceptId;
        }

        /// <inheritdoc />
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}