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
        public string DrinkId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Alcoholic { get; set; }
        public string Glass { get; set; }
        public string Instructions { get; set; }
        public string PhotoUrl { get; set; }
        public string DateModified { get; set; }
        public ICollection<BsonDocument> Ingradients { get; set; }
    }
}
