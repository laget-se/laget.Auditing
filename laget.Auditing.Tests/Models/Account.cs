namespace laget.Auditing.Tests.Models
{
    public class Account
    {
        public int Id { get; set; } = 1;
        public string FirstName { get; set; } = "Jane";
        public string LastName { get; set; } = "Doe";
        public string Email => $"{FirstName.ToLower()}.{LastName.ToLower()}@laget.se";
    }
}
