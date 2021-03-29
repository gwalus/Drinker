using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DataDownloader.Models
{
    /// <summary>
    /// A class representing the mongodb document model.
    /// </summary>
    public class DrinkBsonItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("drinkId")]
        public string DrinkId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("category")]
        public string Category { get; set; }
        [BsonElement("alcoholic")]
        public string Alcoholic { get; set; }
        [BsonElement("glass")]
        public string Glass { get; set; }
        [BsonElement("instructions")]
        public string Instructions { get; set; }
        [BsonElement("photoUrl")]
        public string PhotoUrl { get; set; }
        [BsonElement("dateModified")]
        public string DateModified { get; set; }
        [BsonElement("ingradients")]
        public ICollection<BsonDocument> Ingradients { get; set; }
    }
}
