namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data.Wrappers
{
    internal class ConceptWrapper
    {
        public Data data { get; set; }
        
        public class Data
        {
            public Concept conceptRetrieve { get; set; }
        }
    }
}