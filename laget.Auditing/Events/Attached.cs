using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Attached : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Attached);

        public Attached(string name, object entity)
            : base(name, entity)
        {
        }

        public Attached(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
