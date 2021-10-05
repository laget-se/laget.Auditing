using System;
using System.Linq.Expressions;
using System.Reflection;
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
        int ClubId { get; set; }
        int SiteId { get; set; }
        object Reference { get; set; }
        string Name { get; set; }
        By By { get; set; }
    }

    public abstract class Event : Azure.ServiceBus.Message, IEvent
    {
        [JsonProperty("id")]
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ClubId { get; set; }
        [JsonProperty("siteId", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SiteId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("by")]
        public By By { get; set; }

        [JsonProperty("entity")]
        public object Entity { get; set; }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Description { get; set; }
        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object Reference { get; set; }

        #region Ignored Properties

        [JsonProperty("action"), JsonIgnore]
        public abstract string Action { get; set; }

        [JsonProperty("category"), JsonIgnore]
        public override string Category { get; set; }

        [JsonProperty("source"), JsonIgnore]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }

        #endregion

        protected Event()
        {
        }

        protected Event(string name, object entity)
        {
            Name = name;
            Entity = entity;
        }

        public Event With(Expression<Func<IEvent, object>> expression, object value)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                var property = memberExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(this, value, null);
                }
            }

            if (expression.Body is UnaryExpression unaryExpression)
            {
                var property = (unaryExpression.Operand as MemberExpression)?.Member as PropertyInfo;

                if (property != null)
                {
                    property.SetValue(this, value, null);
                }
            }

            return this;
        }
    }
}
