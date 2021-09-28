using laget.Auditing.Core.Attributes;

namespace laget.Auditing.Core.Tests.Models
{
    public class Account
    {
        [Auditable("id")]
        public int Id { get; set; } = 1;
        [Auditable("firstName")]
        public string FirstName { get; set; } = "Jane";
        [Auditable("lastName")]
        public string LastName { get; set; } = "Doe";
        [Auditable("email")]
        public string Email => $"{FirstName.ToLower()}.{LastName.ToLower()}@laget.se";
        [Auditable]
        public bool IsActive { get; set; } = true;

        public string ShouldNotBeSerialized => "ShouldNotBeSerialized";
    }
}
