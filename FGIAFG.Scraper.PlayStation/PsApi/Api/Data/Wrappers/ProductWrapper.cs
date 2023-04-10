namespace FGIAFG.Scraper.PlayStation.PsApi.Api.Data.Wrappers
{
    internal class ProductWrapper
    {
        public Data data { get; set; }
        
        public class Data
        {
            public Product productRetrieve { get; set; }
        }
    }
}