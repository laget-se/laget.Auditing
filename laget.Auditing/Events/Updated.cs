using laget.Auditing.Core.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Updated : Message
    {
        [JsonProperty("action")]
        public override string Action => Core.Constants.Action.Update.ToString();

        public Updated(object entity)
            : base(entity)
        {
        }
    }
}
