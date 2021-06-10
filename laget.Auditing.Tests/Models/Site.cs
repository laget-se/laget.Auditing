using laget.Auditing.Models.Attributes;

namespace laget.Auditing.Tests.Models
{
    public class Site
    {
        [Auditing("siteId")]
        public int Id { get; set; } = 134049;
        [Auditing("clubId")]
        public int IntDcId { get; set; } = 67347;
        public string Name { get; set; } = "FC GonAce";
    }
}
