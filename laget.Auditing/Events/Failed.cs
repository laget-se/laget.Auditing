using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Failed : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Failed);

        public Failed(string name, object entity)
            : base(name, entity)
        {
        }
    }
}
