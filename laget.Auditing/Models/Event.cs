using System;
using System.Diagnostics;
using System.Reflection;
using System.Linq.Expressions;
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
        string System { get; set; }
        object Reference { get; set; }
        By By { get; set; }
    }

    public abstract class Event : Azure.ServiceBus.Message, IEvent
    {
        [JsonProperty("id")]
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        [JsonProperty("action")]
        public abstract string Action { get; set; }
        [JsonProperty("clubId", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ClubId { get; set; }
        [JsonProperty("siteId", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int SiteId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("by")]
        public By By { get; set; }
        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("entity")]
        public object Entity { get; set; }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual string Description { get; set; }
        [JsonProperty("reference", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public virtual object Reference { get; set; }

        #region Ignored Properties

        [JsonProperty("category"), JsonIgnore]
        public override string Category { get; set; }
        [JsonProperty("source"), JsonIgnore]
        public override Microsoft.Azure.ServiceBus.Message Source { get; set; }
        [JsonProperty("type"), JsonIgnore]
        public override string Type { get; set; }

        #endregion

        protected Event()
        {
        }

        protected Event(string name, object entity)
        {
            var assembly = GetCallingAssembly();

            Name = name;
            Entity = entity;
            System = assembly.GetName().Name;
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

        private static Assembly GetCallingAssembly()
        {
            var me = Assembly.GetExecutingAssembly();
            var st = new StackTrace(false);
            var frames = st.GetFrames();

            if (frames == null)
                return null;

            foreach (var frame in frames)
            {
                var m = frame.GetMethod();
                if (m != null && m.DeclaringType != null && m.DeclaringType.Assembly != me)
                    return m.DeclaringType.Assembly;
            }

            return null;
        }
    }
}
