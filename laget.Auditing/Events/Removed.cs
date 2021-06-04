using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Auditor.Events
{
    public class Removed : Message
    {
        [JsonProperty("action")]
        public override string Action => Models.Constants.Action.Remove.ToString();

        public Removed(object entity)
            : base(entity)
        {
        }
    }
}
