namespace DrinkerAPI.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Measure { get; set; }
        public Coctail CoctailId { get; set; }
    }
}
