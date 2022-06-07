using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Disconnected : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Disconnected);

        public Disconnected(string name, object entity)
            : base(name, entity)
        {
        }

        public Disconnected(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
