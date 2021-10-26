using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Published : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Published);

        public Published(string name, object entity)
            : base(name, entity)
        {
        }

        public Published(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
