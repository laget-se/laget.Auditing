using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Inserted : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Inserted);

        public Inserted(string name, object entity)
            : base(name, entity)
        {
        }

        public Inserted(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
