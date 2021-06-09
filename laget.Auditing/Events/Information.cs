using laget.Auditing.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Information : Message
    {
        [JsonProperty("action")]
        public override string Action => Models.Constants.Action.Information.ToString();

        public Information(object entity, string description)
            : base(entity)
        {
            Description = description;
        }
    }
}
