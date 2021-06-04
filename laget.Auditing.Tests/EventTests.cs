﻿using laget.Auditing.Auditor.Events;
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
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };
            var site = new Site();

            var message = new Added(account)
                .With(x => x.By, by)
                .With(x => x.To, site);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Account", message.Type);
            Assert.Equal("Created", message.Action);
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
            Assert.Equal("Account", message.Type);
            Assert.Equal("Created", message.Action);
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
            Assert.Equal("Account", message.Type);
            Assert.Equal("Deleted", message.Action);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);
        }

        [Fact]
        public void ShouldMakeInformationEvent()
        {
            var account = new Account();
            var by = new By { Id = 2, Name = "John Doe" };

            var message = new Information(account, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate rhoncus mattis. Cras malesuada consectetur mi, quis feugiat lorem pellentesque a.")
                .With(x => x.By, by);

            // Assert
            Assert.NotEmpty(message.Id);
            Assert.Equal("Account", message.Type);
            Assert.Equal("Information", message.Action);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
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
            Assert.Equal("Account", message.Type);
            Assert.Equal("Removed", message.Action);
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
            Assert.Equal("Account", message.Type);
            Assert.Equal("Updated", message.Action);
            Assert.Equal(account, message.For);
            Assert.Equal(by, message.By);
            Assert.Equal(site, message.From);
        }
    }
}
