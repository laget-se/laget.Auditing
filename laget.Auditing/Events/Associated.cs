using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Associated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Associated);

        public Associated(string name, object entity)
            : base(name, entity)
        {
        }

        public Associated(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
