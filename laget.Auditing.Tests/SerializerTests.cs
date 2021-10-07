using System.Globalization;
using System.Threading;
using laget.Auditing.Events;
using laget.Auditing.Models;
using laget.Auditing.Tests.Models;
using Xunit;

namespace laget.Auditing.Tests
{
    public class SerializerTests
    {
        public SerializerTests()
        {
            Thread.CurrentThread.CurrentCulture.DateTimeFormat = new DateTimeFormatInfo
            {
            };
        }

        [Fact(Skip = "We need to provide a way to test the serializer with datetime (createdAt)!")]
        public void ShouldSerialize()
        {
            var name = "Jane Doe";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(name, account)
                .With(x => x.Reference, by);

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
  ""createdAt"": ""2021-09-29T09:44:45.9268786+02:00""
}";

            //var actual = Serializer.Serialize(message);

            //Assert.Equal(expected, actual);
        }
    }
}
