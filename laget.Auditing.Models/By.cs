using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace laget.Auditing.Models
{
    public class By
    {
        [BsonElement("id"), JsonProperty("id")]
        public int Id { get; set; }
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        [BsonElement("superuser"), JsonProperty("superuser")]
        public bool Superuser { get; set; } = false;
    }
}
