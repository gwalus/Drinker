using MongoDB.Bson.Serialization.Attributes;

namespace DataDownloader.Models
{
    /// <summary>
    /// Drink ingredient model.
    /// </summary>
    public class Ingredient
    {
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("measure")]
        public string Measure { get; set; }
    }
}
