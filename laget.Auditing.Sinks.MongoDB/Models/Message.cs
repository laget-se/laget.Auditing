using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace laget.Auditing.Sinks.MongoDB.Models
{
    public class Message
    {
        [BsonElement("id"), BsonId, BsonIgnoreIfDefault, BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = new ObjectId();
        [JsonProperty("id"), BsonElement("sourceId")]
        public string SourceId { get; set; }
        [BsonElement("clubId")]
        public int ClubId { get; set; }
        [BsonElement("siteId")]
        public int SiteId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("by")]
        public object By
        {
            get => _by;
            set
            {
                _by = value;
                _by = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(By));
            }
        }
        private object _by;
        [BsonElement("description")]
        public string Description { get; set; }
        [BsonElement("entity")]
        public object Entity
        {
            get => _entity;
            set
            {
                _entity = value;
                _entity = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(Entity));
            }
        }
        private object _entity;
        [BsonElement("reference")]
        public object Reference
        {
            get => _reference;
            set
            {
                _reference = value;
                _reference = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(Reference));
            }
        }
        private object _reference;

        [BsonElement("createdAt"), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }
        [BsonElement("persistedAt"), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime PersistedAt { get; set; } = DateTime.Now;
    }
}
