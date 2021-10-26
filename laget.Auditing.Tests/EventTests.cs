using laget.Auditing.Events;
using laget.Auditing.Models;
using laget.Auditing.Tests.Models;
using Xunit;

namespace laget.Auditing.Tests
{
    public class EventTests
    {
        [Fact]
        public void ShouldMakeAddedEvent()
        {
            var name = "ShouldMakeAddedEvent";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Added", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
        }

        [Fact]
        public void ShouldMakeCreateEvent()
        {
            var name = "ShouldMakeCreateEvent";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Created(name, account)
                .With(x => x.By, by);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Created", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
        }

        [Fact]
        public void ShouldMakeDeleteEvent()
        {
            var name = "ShouldMakeDeleteEvent";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Deleted(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Deleted", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
        }

        [Fact]
        public void ShouldMakeFailedEvent()
        {
            var name = "ShouldMakeFailedEvent";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Failed(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Failed", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
        }

        [Fact]
        public void ShouldMakeInformationEvent()
        {
            var name = "ShouldMakeInformationEvent";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.";
            var message = new Information(name, account, description)
                .With(x => x.By, by);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Information", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(description, message.Description);
        }

        [Fact]
        public void ShouldMakeRemovedEvent()
        {
            var name = "ShouldMakeRemovedEvent";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Removed(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Removed", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
        }

        [Fact]
        public void ShouldMakeSuccededUpdate()
        {
            var name = "ShouldMakeSuccededUpdate";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Succeded(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Succeded", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
        }

        [Fact]
        public void ShouldMakeRemovedUpdate()
        {
            var name = "ShouldMakeRemovedUpdate";
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Updated(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Updated", message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(account, message.Entity);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
        }
    }
}
