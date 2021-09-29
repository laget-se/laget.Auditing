using laget.Auditing.Core.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Updated : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Updated);

        public Updated(object entity)
            : base(entity)
        {
        }
    }
}
