using Newtonsoft.Json;

namespace laget.Auditing.Models
{
    public class By
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ip")]
        public string Ip { get; set; }
        [JsonProperty("referenceId")]
        public string ReferenceId { get; set; }
        [JsonProperty("superadmin")]
        public bool Superadmin { get; set; } = false;

        public static By System => new By
        {
            Name = "SYSTEM",
            Superadmin = true
        };
    }
}
