using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Cloned : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Cloned);

        public Cloned(string name, object entity)
            : base(name, entity)
        {
        }

        public Cloned(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
