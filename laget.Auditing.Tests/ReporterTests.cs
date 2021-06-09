using System.Threading.Tasks;
using laget.Auditing.Tests.Models;
using MongoDB.Driver;
using Xunit;

namespace laget.Auditing.Tests
{
    public class ReporterTests
    {
        private readonly IReporter<Account> _reporter;

        public ReporterTests()
        {
            _reporter = new Reporter<Account>(new MongoUrl(""));
        }

        [Fact(Skip = "We need to mock the auditor")]
        public void ShouldFindEvents()
        {
            var filter = Builders<Auditing.Models.Record>.Filter.Empty;
            var records = _reporter.Find(filter);

            // Assert
            //TODO: Make assertions
        }

        [Fact(Skip = "We need to mock the auditor")]
        public async Task ShouldFindEventsAsync()
        {
            var filter = Builders<Auditing.Models.Record>.Filter.Empty;

            var records = await _reporter.FindAsync(filter);

            // Assert
            //TODO: Make assertions
        }
    }
}
