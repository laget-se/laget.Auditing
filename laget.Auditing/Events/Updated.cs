using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Updated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Updated);

        public Updated(string name, object entity)
            : base(name, entity)
        {
        }
    }
}
