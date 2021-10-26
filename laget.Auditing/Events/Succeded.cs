using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Succeded : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Succeded);

        public Succeded(string name, object entity)
            : base(name, entity)
        {
        }
    }
}
