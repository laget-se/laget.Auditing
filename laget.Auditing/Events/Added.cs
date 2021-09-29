using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Added : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Added);
        
        public Added(object entity)
            : base(entity)
        {
        }
    }
}
