using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Information : Event
    {
        [JsonProperty("action")]
        public override string Action { get; set; } = nameof(Information);

        public Information(object entity, string description)
            : base(entity)
        {
            Description = description;
        }
    }
}
