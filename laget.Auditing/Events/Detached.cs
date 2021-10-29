using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Detached : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Detached);

        public Detached(string name, object entity)
            : base(name, entity)
        {
        }

        public Detached(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
