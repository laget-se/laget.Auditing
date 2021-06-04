using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Auditor.Events
{
    public class Deleted : Message
    {
        [JsonProperty("action")]
        public override string Action => Models.Constants.Action.Delete.ToString();

        public Deleted(object entity)
            : base(entity)
        {
        }
    }
}
