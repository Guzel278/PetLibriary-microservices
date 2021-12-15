using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Libriary.API.Entities
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string PublishingHouse { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        
        
    }
}
