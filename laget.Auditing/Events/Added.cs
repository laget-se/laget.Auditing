using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Auditor.Events
{
    public class Added : Message
    {
        [JsonProperty("action")]
        public override string Action => Models.Constants.Action.Added.ToString();

        public Added(object entity)
            : base(entity)
        {
        }
    }
}
