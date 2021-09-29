using System;
using System.Linq.Expressions;
using System.Reflection;
using laget.Auditing.Converters;
using Newtonsoft.Json;

namespace laget.Auditing.Models
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
        [JsonProperty("id")]
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("source")]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }
        [JsonProperty("action"), JsonIgnore]
        public abstract string Action { get; set; }

        [JsonProperty("by")]
        public virtual By By { get; set; }
        [JsonConverter(typeof(AuditingConverter))]
        [JsonProperty("for")]
        public virtual object For { get; set;  }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Description { get; set; }

        [JsonConverter(typeof(AuditingConverter))]
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object From { get; set; }
        [JsonConverter(typeof(AuditingConverter))]
        [JsonProperty("in", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object In { get; set; }
        [JsonConverter(typeof(AuditingConverter))]
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
    }
}
