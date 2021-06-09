
using laget.Auditing.Attributes;

namespace laget.Auditing.Tests.Models
{
    public class Site
    {
        [Auditing("SiteId")]
        public int Id { get; set; } = 134049;
        [Auditing("ClubId")]
        public int IntDcId { get; set; } = 67347;
        public string Name { get; set; } = "FC GonAce";
    }
}
