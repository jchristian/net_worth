using System.Linq;
using core.importers;
using core.importers.matches;
using Moq;
using Xunit;

namespace core.tests.importers
{
    public class GenericBrokerageTransactionImporterTests
    {
        [Fact]
        public void when_there_is_a_match()
        {
            var text = "this is some text";
            var match = new Mock<ITransactionImporterMatch>();
            match.Setup(x => x.Matches(text)).Returns(true);

            var matches = new Mock<TransactionImporterMatches>();
            matches.Setup(x => x.ImporterMatches).Returns(() => new[] { match.Object });

            //Act
            new GenericBrokerageTransactionImporter(matches.Object).Import(text);

            //Assert
            match.Verify(x => x.Import(text));
        }

        [Fact]
        public void when_there_is_no_match()
        {
            var text = "this is some text";
            var match = new Mock<ITransactionImporterMatch>();
            match.Setup(x => x.Matches(text)).Returns(true);

            var matches = new Mock<TransactionImporterMatches>();
            matches.Setup(x => x.ImporterMatches).Returns(() => new[] { match.Object });

            //Act
            Assert.Throws<InvalidFileForImportException>(() => new GenericBrokerageTransactionImporter(matches.Object).Import("this text doesn't match"));
        }
    }
}