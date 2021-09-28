using laget.Auditing.Core;
using laget.Auditing.Core.Models;
using laget.Auditing.Events;
using laget.Auditing.Tests.Models;
using Xunit;

namespace laget.Auditing.Tests
{
    public class SerializerTests
    {
        [Fact(Skip = "We need to provide a way to test the serializer with datetime (createdAt)!")]
        public void ShouldSerialize()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);

            var expected = @"{
  ""by"": {
    ""id"": 2,
    ""name"": ""John Doe"",
    ""superadmin"": false
  },
  ""for"": {
    ""id"": 1,
    ""firstName"": ""Jane"",
    ""lastName"": ""Doe"",
    ""email"": ""jane.doe@laget.se"",
    ""isActive"": true
  },
  ""to"": {
    ""id"": 134049,
    ""intDcId"": 67347,
    ""name"": ""FC GonAce""
  },
  ""type"": ""Added"",
  ""category"": ""Account"",
  ""createdAt"": ""2021-07-14T10:19:42.1154758+02:00""
}";
            var actual = Serializer.Serialize(message);

            Assert.Equal(expected, actual);
        }
    }
}
