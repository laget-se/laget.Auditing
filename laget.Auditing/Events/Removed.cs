using laget.Auditing.Core.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Removed : Message
    {
        [JsonProperty("action")]
        public override string Action => Core.Constants.Action.Remove.ToString();

        public Removed(object entity)
            : base(entity)
        {
        }
    }
}
