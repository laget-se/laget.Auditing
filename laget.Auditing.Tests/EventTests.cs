using laget.Auditing.Core.Models;
using laget.Auditing.Events;
using laget.Auditing.Tests.Models;
using Xunit;

namespace laget.Auditing.Tests
{
    public class EventTests
    {
        [Fact]
        public void ShouldMakeAddedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Added", message.Action);
            Assert.Equal("Account", message.Category);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.To);
        }

        [Fact]
        public void ShouldMakeCreateEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Created(account)
                .With(x => x.By, by);
            
            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Created", message.Action);
            Assert.Equal("Account", message.Category);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
        }

        [Fact]
        public void ShouldMakeDeleteEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Deleted(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Deleted", message.Action);
            Assert.Equal("Account", message.Category);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);
        }

        [Fact]
        public void ShouldMakeInformationEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.";
            var message = new Information(account, description)
                .With(x => x.By, by);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Information", message.Action);
            Assert.Equal("Account", message.Category);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(description, message.Description);
        }

        [Fact]
        public void ShouldMakeRemovedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Removed(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Removed", message.Action);
            Assert.Equal("Account", message.Category);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);
        }

        [Fact]
        public void ShouldMakeRemovedUpdate()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Updated(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Updated", message.Action);
            Assert.Equal("Account", message.Category);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);
        }
    }
}
