using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Unpublished : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Unpublished);

        public Unpublished(string name, object entity)
            : base(name, entity)
        {
        }

        public Unpublished(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
