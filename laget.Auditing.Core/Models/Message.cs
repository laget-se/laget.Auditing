using System;
using System.Linq.Expressions;
using System.Reflection;
using laget.Auditing.Core.Converters;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace laget.Auditing.Core.Models
{
    /// <summary>
    /// We need to implement this interface to be able to select which properties that
    /// should be exposed when using the With(Expression<Func<IEvent, object>> expression, object value)
    /// method.
    /// </summary>
    public interface IEvent
    {
        string Category { get; set; }

        By By { get; set; }
        object From { get; set; }
        object In { get; set; }
        object To { get; set; }
    }
    
    public abstract class Event : Azure.ServiceBus.Message, IEvent
    {
        [JsonProperty("id"), JsonIgnore, BsonIgnore]
        public override string Id => Guid.NewGuid().ToString();
        [JsonProperty("source"), JsonIgnore, BsonIgnore]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }
        [JsonProperty("action"), JsonIgnore, BsonIgnore]
        public abstract string Action { get; set; }

        [JsonProperty("by")]
        public virtual By By { get; set; }
        [JsonProperty("for"), JsonConverter(typeof(AuditingConverter))]
        public virtual object For { get; set;  }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Description { get; set; }
        
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object From { get; set; }
        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object In { get; set; }
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object To { get; set; }

        protected Event()
        {
        }

        protected Event(object entity)
        {
            Category = entity.GetType().Name;
            For = entity;
            Type = Action;
        }

        public Event With(Expression<Func<IEvent, object>> expression, object value)
        {
            if (expression.Body is MemberExpression memberSelectorExpression)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(this, value, null);
                }
            }

            return this;
        }

        [JsonIgnore]
        public Record ToRecord => new Record
        {
            By = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(By)),
            CreatedAt = CreatedAt,
            Description = Description,
            For = BsonSerializer.Deserialize<object>(Serializer.Serialize(For)),
            From = BsonSerializer.Deserialize<object>(Serializer.Serialize(From)),
            In = BsonSerializer.Deserialize<object>(Serializer.Serialize(In)),
            To = BsonSerializer.Deserialize<object>(Serializer.Serialize(To)),
            Type = Type
        };
    }
}
