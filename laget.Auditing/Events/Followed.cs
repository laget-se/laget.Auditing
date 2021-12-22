using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Followed : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Followed);

        public Followed(string name, object entity)
            : base(name, entity)
        {
        }

        public Followed(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
