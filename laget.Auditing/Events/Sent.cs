using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Sent : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Sent);

        public Sent(string name, object entity)
            : base(name, entity)
        {
        }

        public Sent(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
