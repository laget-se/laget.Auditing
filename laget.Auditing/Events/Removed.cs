using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Removed : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Removed);

        public Removed(string name, object entity)
            : base(name, entity)
        {
        }

        public Removed(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
