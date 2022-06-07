using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Disassociated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Disassociated);

        public Disassociated(string name, object entity)
            : base(name, entity)
        {
        }

        public Disassociated(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
