using System;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace laget.Auditing.Models
{
    /// <summary>
    /// We need to implement this interface to be able to select which properties that
    /// should be exposed when using the With(Expression<Func<IRecord, object>> expression, object value)
    /// method.
    /// </summary>
    public interface IMessage
    {
        By By { get; set; }
        object From { get; set; }
        object In { get; set; }
        object To { get; set; }
    }

    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class Message : Azure.ServiceBus.Message, IMessage
    {
        [JsonProperty("id"), JsonIgnore, BsonIgnore]
        public override string Id => Guid.NewGuid().ToString();
        [JsonProperty("source"), JsonIgnore, BsonIgnore]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }
        [JsonProperty("action"), JsonIgnore, BsonIgnore]
        public virtual string Action => Constants.Action.Information.ToString();

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
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object To { get; set; }

        public Message()
        {
        }

        public Message(object entity)
        {
            Category = entity.GetType().Name;
            For = entity;
            Type = Action;
        }

        public Message With(Expression<Func<IMessage, object>> expression, object value)
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
            For = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(For)),
            Description = Description,
            From = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(From)),
            In = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(In)),
            To = BsonSerializer.Deserialize<object>(JsonConvert.SerializeObject(To)),
            Type = Type
        };
    }
}
