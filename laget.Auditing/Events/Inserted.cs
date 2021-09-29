using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Inserted : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Inserted);

        public Inserted(object entity)
            : base(entity)
        {
        }
    }
}
