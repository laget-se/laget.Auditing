using laget.Auditing.Core.Attributes;

namespace laget.Auditing.Tests.Models
{
    public class Site
    {
        [Auditable("siteId")]
        public int Id { get; set; } = 134049;
        [Auditable("clubId")]
        public int IntDcId { get; set; } = 67347;
        public string Name { get; set; } = "FC GonAce";
    }
}
