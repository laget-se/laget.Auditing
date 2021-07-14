using laget.Auditing.Core.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Deleted : Message
    {
        [JsonProperty("action")]
        public override string Action => Core.Constants.Action.Delete.ToString();

        public Deleted(object entity)
            : base(entity)
        {
        }
    }
}
