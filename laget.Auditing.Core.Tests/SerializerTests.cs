using laget.Auditing.Core.Models;
using laget.Auditing.Core.Tests.Models;
using Xunit;

namespace laget.Auditing.Core.Tests
{
    public class SerializerTests
    {
        [Fact]
        public void ShouldSerialize()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);


            var expected = "{\r\n  \"id\": 1,\r\n  \"firstName\": \"Jane\",\r\n  \"lastName\": \"Doe\",\r\n  \"email\": \"jane.doe@laget.se\",\r\n  \"isActive\": true,\r\n  \"shouldNotBeSerialized\": \"ShouldNotBeSerialized\"\r\n}";
            var actual = Serializer.Serialize(account);

            Assert.Equal(expected, actual);
        }
    }
}
