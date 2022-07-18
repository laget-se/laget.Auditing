using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Purged : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Published);

        public Purged(string name, object entity)
            : base(name, entity)
        {
        }

        public Purged(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
