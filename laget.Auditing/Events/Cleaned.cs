using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Cleaned : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Cloned);

        public Cleaned(string name, object entity)
            : base(name, entity)
        {
        }

        public Cleaned(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
