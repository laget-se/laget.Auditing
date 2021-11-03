using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Activated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Activated);

        public Activated(string name, object entity)
            : base(name, entity)
        {
        }

        public Activated(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
