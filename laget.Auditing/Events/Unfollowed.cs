using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Unfollowed : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Unfollowed);

        public Unfollowed(string name, object entity)
            : base(name, entity)
        {
        }

        public Unfollowed(string name, object entity, string description)
            : base(name, entity)
        {
            Description = description;
        }
    }
}
