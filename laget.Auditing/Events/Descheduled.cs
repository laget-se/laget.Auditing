using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Descheduled : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Descheduled);

        public Descheduled(string name, object entity)
            : base(name, entity)
        {
        }

        public Descheduled(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
