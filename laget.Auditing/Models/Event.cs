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
        string Category { get; set; }
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

        [JsonProperty("source"), JsonIgnore]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }

        [JsonProperty("action"), JsonIgnore]
        public abstract string Action { get; set; }

        [JsonProperty("by")]
        public virtual By By { get; set; }

        [JsonProperty("for")]
        public virtual object For { get; set; }

        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Description { get; set; }

        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object Reference { get; set; }

        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual int ClubId { get; set; }

        [JsonProperty("siteId", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual int SiteId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Name { get; set; }

        #region ignore

        [JsonProperty("category"), JsonIgnore]
        public override string Category { get; set; }

        #endregion

        protected Event()
        {
        }

        protected Event(string name, object entity)
        {
            Name = name;
            For = entity;
            Type = Action;
            Category = entity.GetType().Name;
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
