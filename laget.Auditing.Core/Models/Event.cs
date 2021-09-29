using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace laget.Auditing.Core.Models
{
    public class Event : Azure.ServiceBus.Message
    {
        [JsonProperty("id"), JsonIgnore]
        public override string Id { get; set; }
        [JsonProperty("source"), JsonIgnore]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }

        [JsonProperty("by")]
        public virtual By By { get; set; }
        [JsonProperty("for")]
        public virtual object For { get; set;  }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Description { get; set; }
        
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object From { get; set; }
        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object In { get; set; }
        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object On { get; set; }
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object To { get; set; }
        
        [JsonIgnore]
        public Record ToRecord => new Record
        {
            By = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(By)),
            CreatedAt = CreatedAt,
            Description = Description,
            For = BsonSerializer.Deserialize<object>(Serializer.Serialize(For)),
            From = BsonSerializer.Deserialize<object>(Serializer.Serialize(From)),
            In = BsonSerializer.Deserialize<object>(Serializer.Serialize(In)),
            On = BsonSerializer.Deserialize<object>(Serializer.Serialize(On)),
            To = BsonSerializer.Deserialize<object>(Serializer.Serialize(To)),
            Type = Type
        };
    }
}
