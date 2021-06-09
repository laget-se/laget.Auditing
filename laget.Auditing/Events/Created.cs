using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Created : Message
    {
        [JsonProperty("action")]
        public override string Action => Models.Constants.Action.Create.ToString();

        public Created(object entity)
            : base(entity)
        {
        }
    }
}
