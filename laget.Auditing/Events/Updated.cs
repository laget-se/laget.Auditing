using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Auditor.Events
{
    public class Updated : Message
    {
        [JsonProperty("action")]
        public override string Action => Models.Constants.Action.Update.ToString();

        public Updated(object entity)
            : base(entity)
        {
        }
    }
}
