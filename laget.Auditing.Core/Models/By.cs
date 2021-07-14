using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace laget.Auditing.Core.Models
{
    public class By
    {
        [BsonElement("id"), JsonProperty("id")]
        public int Id { get; set; }
        [BsonElement("name"), JsonProperty("name")]
        public string Name { get; set; }
        [BsonElement("superadmin"), JsonProperty("superadmin")]
        public bool Superadmin { get; set; } = false;
    }
}
