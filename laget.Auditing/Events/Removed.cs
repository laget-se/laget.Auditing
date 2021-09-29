using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Removed : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Removed);

        public Removed(object entity)
            : base(entity)
        {
        }
    }
}
