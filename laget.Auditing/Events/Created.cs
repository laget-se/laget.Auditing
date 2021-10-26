using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Created : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Created);

        public Created(string name, object entity)
            : base(name, entity)
        {
        }
    }
}
