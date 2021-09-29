using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace laget.Auditing.Core.Models
{
    public class Record
    {
        [BsonElement("id"), BsonId, BsonIgnoreIfDefault, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("by")]
        public object By { get; set; }
        [BsonElement("for"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public object For { get; set;  }
        [BsonElement("description"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public string Description { get; set; }

        [BsonElement("from"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public object From { get; set; }
        [BsonElement("in"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public object In { get; set; }
        [BsonElement("on"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public object On { get; set; }
        [BsonElement("to"), BsonIgnoreIfDefault, BsonIgnoreIfNull]
        public object To { get; set; }

        [BsonElement("createdAt"), BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }
    }
}
