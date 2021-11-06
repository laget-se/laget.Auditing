using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Restored : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Restored);

        public Restored(string name, object entity)
            : base(name, entity)
        {
        }

        public Restored(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
