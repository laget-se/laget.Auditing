using laget.Auditing.Models.Attributes;

namespace laget.Auditing.Tests.Models
{
    public class Account
    {
        [Auditing("id")]
        public int Id { get; set; } = 1;
        [Auditing("firstName")]
        public string FirstName { get; set; } = "Jane";
        [Auditing("lastName")]
        public string LastName { get; set; } = "Doe";
        [Auditing("email")]
        public string Email => $"{FirstName.ToLower()}.{LastName.ToLower()}@laget.se";

        public string ShouldNotBeSerialized => "ShouldNotBeSerialized";
    }
}
