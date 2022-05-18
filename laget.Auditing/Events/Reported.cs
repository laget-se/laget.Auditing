using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Reported : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Reported);

        public Reported(string name, object entity)
            : base(name, entity)
        {
        }

        public Reported(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
