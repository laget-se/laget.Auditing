using laget.Auditing.Core.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Created : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Created);

        public Created(object entity)
            : base(entity)
        {
        }
    }
}
