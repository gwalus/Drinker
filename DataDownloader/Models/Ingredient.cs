using MongoDB.Bson.Serialization.Attributes;

namespace DataDownloader.Models
{
    /// <summary>
    /// Drink ingredient model.
    /// </summary>
    public class Ingredient
    {
        [BsonElement]
        public string Name { get; set; }
        [BsonElement]
        public string Measure { get; set; }
    }
}
