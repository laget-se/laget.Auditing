using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Enqueued : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Enqueued);

        public Enqueued(string name, object entity)
            : base(name, entity)
        {
        }

        public Enqueued(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
