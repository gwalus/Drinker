namespace DrinkerAPI.Helpers
{
    public class CoctailParams : PaginationParams
    {
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
    }
}
