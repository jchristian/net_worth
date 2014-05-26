using core.importers;
using developwithpassion.specifications.nsubstitue;
using FluentAssertions;
using Machine.Specifications;
using StructureMap;
using ui.web.DependencyResolution;

namespace ui.web.tests.DependencyResolution
{
    public class CoreRegistrySpecs
    {
        public abstract class concern : Observes<CoreRegistry> {}

        [Subject(typeof(DependencyBootstrapper))]
        public class when_bootstrapping_the_container : concern
        {
            Establish c = () =>
            {
                DependencyBootstrapper.Bootstrap();
            };

            Because of = () =>
                importer = ObjectFactory.GetInstance<VanguardTransactionImporter>();

            It should_return_a_vanguard_transaction_importer = () =>
                importer.Should().BeOfType<VanguardTransactionImporter>();
            
            static VanguardTransactionImporter importer;
        }
    }
}
