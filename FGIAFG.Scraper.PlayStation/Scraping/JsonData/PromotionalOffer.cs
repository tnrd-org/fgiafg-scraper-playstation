namespace FGIAFG.Scraper.PlayStation.Scraping.JsonData
{
    internal class PromotionalOffer
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DiscountSetting DiscountSetting { get; set; }
    }
}