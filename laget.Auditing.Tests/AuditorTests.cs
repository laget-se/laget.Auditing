using laget.Auditing.Events;
using laget.Auditing.Models;
using laget.Auditing.Tests.Models;
using laget.Azure.ServiceBus.Topic;
using Moq;
using System.Threading;
using System.Threading.Tasks;
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
            var name = nameof(Account);

            var message = new Added(name, account)
                .With(x => x.ClubId, 67347)
                .With(x => x.SiteId, 134049)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Added), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendAddedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Added(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Added), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendCreateEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var name = nameof(Account);

            var message = new Created(name, account)
                .With(x => x.By, by);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Created), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendCreateEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var name = nameof(Account);

            var message = new Created(name, account)
                .With(x => x.By, by);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Created), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendDeletedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Deleted(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Deleted), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendDeletedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Deleted(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Deleted), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendInformationEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var name = nameof(Account);

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.";
            var message = new Information(name, account, description)
                .With(x => x.By, by);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Information), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal(description, message.Description);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendInformationEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var name = nameof(Account);

            var description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.";
            var message = new Information(name, account, description)
                .With(x => x.By, by);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Information), message.Action);
            Assert.Equal(by, message.By);
            Assert.Equal(description, message.Description);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendRemovedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Removed(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Removed), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendRemovedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Removed(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Removed), message.Action);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldSendUpdatedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Updated(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            _auditor.Send(message);

            // Assert
            Assert.Equal(nameof(Updated), message.Action);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendUpdatedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();
            var name = nameof(Account);

            var message = new Updated(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Updated), message.Action);
            Assert.Equal(name, message.Name);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldSendEventWithCustomObjectAsync()
        {
            var name = "Account";
            var account = new
            {
                id = 123,
                name = "Jane Doe"
            };
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new
            {
                id = 123,
                name = "FC GonAce"
            };

            var message = new Created(name, account)
                .With(x => x.By, by)
                .With(x => x.Reference, site);
            var json = message.Serialize();

            await _auditor.SendAsync(message);

            // Assert
            Assert.Equal(nameof(Created), message.Action);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.Reference);
            Assert.Equal("laget.Auditing.Tests", message.System);

            // Verify
            _topicSender.Verify(x => x.SendAsync(json, It.IsAny<CancellationToken>()), Times.Once);
            _topicSender.VerifyNoOtherCalls();
        }
    }
}
