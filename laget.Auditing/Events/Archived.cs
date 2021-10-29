using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Archived : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Archived);

        public Archived(string name, object entity)
            : base(name, entity)
        {
        }

        public Archived(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
