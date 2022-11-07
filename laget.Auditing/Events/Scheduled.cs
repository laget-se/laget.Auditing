using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Scheduled : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Scheduled);

        public Scheduled(string name, object entity)
            : base(name, entity)
        {
        }

        public Scheduled(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
