using core.importers;
using FluentAssertions;
using StructureMap;
using ui.web.DependencyResolution;
using Xunit;

namespace ui.web.tests.DependencyResolution
{
    public class CoreRegistryTests
    {
        [Fact]
        public void when_bootstrapping_the_container()
        {
            DependencyBootstrapper.Bootstrap();

            var importer = ObjectFactory.GetInstance<VanguardTransactionImporter>();

            importer.Should().BeOfType<VanguardTransactionImporter>();
        }
    }
}
