using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Migrated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Migrated);

        public Migrated(string name, object entity)
            : base(name, entity)
        {
        }

        public Migrated(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
