using System.Threading.Tasks;
using laget.Auditing.Core.Models;
using laget.Auditing.Events;
using laget.Auditing.Tests.Models;
using laget.Azure.ServiceBus.Topic;
using Moq;
using Xunit;

namespace laget.Auditing.Tests
{
    public class AuditorTests
    {
        private readonly IAuditor _auditor;
        private readonly Mock<ITopicSender> _topicSender;

        public AuditorTests()
        {
            _topicSender = new Mock<ITopicSender>();
            _auditor = new Auditor(_topicSender.Object);
        }

        [Fact]
        public void ShouldSendAddedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Added), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendAddedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Added), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendCreateEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Created(account)
                .With(x => x.By, by);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Created), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendCreateEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Created(account)
                .With(x => x.By, by);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Created), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendDeletedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Deleted(account)
                .With(x => x.By, by)
                .With(x => x.From, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Deleted), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendDeletedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Deleted(account)
                .With(x => x.By, by)
                .With(x => x.From, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Deleted), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendInformationEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.";
            var message = new Information(account, description)
                .With(x => x.By, by);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Information), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(description, message.Description);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendInformationEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.";
            var message = new Information(account, description)
                .With(x => x.By, by);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Information), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(description, message.Description);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendRemovedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Removed(account)
                .With(x => x.By, by)
                .With(x => x.From, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Removed), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendRemovedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Removed(account)
                .With(x => x.By, by)
                .With(x => x.From, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Removed), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendUpdatedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Updated(account)
                .With(x => x.By, by)
                .With(x => x.In, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Updated), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.In);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendUpdatedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Updated(account)
                .With(x => x.By, by)
                .With(x => x.In, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Updated), message.Action);
            Assert.Equal(nameof(Account), message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.In);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendEventWithCustomObjectAsync()
        {
            var account = new
            {
                id = 123,
                name = "Jane Doe"
            };
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Created(account)
                .With(x => x.Category, "account")
                .With(x => x.By, by)
                .With(x => x.In, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Created), message.Action);
            Assert.Equal("account",message.Category);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.In);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }
    }
}
