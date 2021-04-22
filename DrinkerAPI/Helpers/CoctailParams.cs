namespace DrinkerAPI.Helpers
{
    public class CoctailParams : PaginationParams
    {
        public string Category { get; set; } = "Ordinary Drink";
        public string Alcoholic { get; set; } = "Alcoholic";
        public string Glass { get; set; }
    }
}
