using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Deactivated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Deactivated);

        public Deactivated(string name, object entity)
            : base(name, entity)
        {
        }

        public Deactivated(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
