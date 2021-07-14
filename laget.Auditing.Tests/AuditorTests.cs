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

        public AuditorTests()
        {
            var topicSender = new Mock<ITopicSender>();

            _auditor = new Auditor(topicSender.Object);
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldSendAddedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);

            _auditor.Send(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldSendAddedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);

            await _auditor.SendAsync(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldSendCreateEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Created(account)
                .With(x => x.By, by);

            _auditor.Send(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldSendCreateEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Created(account)
                .With(x => x.By, by);

            await _auditor.SendAsync(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldSendDeletedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Deleted(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            _auditor.Send(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldSendDeletedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Deleted(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            await _auditor.SendAsync(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldSendInformationEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Information(account, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.")
                .With(x => x.By, by);

            _auditor.Send(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldSendInformationEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Information(account, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.")
                .With(x => x.By, by);

            await _auditor.SendAsync(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldSendRemovedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Removed(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            _auditor.Send(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldSendRemovedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Removed(account)
                .With(x => x.By, by)
                .With(x => x.From, site);

            await _auditor.SendAsync(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldSendUpdatedEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Updated(account)
                .With(x => x.By, by)
                .With(x => x.In, site);

            _auditor.Send(message);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldSendUpdatedEventAsync()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Updated(account)
                .With(x => x.By, by)
                .With(x => x.In, site);

            await _auditor.SendAsync(message);

            // Assert
            //TODO: Make assertions
        }
    }
}
