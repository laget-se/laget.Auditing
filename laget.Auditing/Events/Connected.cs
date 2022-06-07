using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Connected : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Connected);

        public Connected(string name, object entity)
            : base(name, entity)
        {
        }

        public Connected(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
