using laget.Auditing.Core.Models;
using Newtonsoft.Json;

namespace laget.Auditing.Events
{
    public class Added : Message
    {
        [JsonProperty("action")]
        public override string Action =>  Core.Constants.Action.Added.ToString();

        public Added(object entity)
            : base(entity)
        {
        }
    }
}
