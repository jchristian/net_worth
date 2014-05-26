using core.importers.parsers;
using core.importers.persisters;
using data.models.contexts;
using data.models.write;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace ui.web.DependencyResolution
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<CSVFileParser>();
                x.SingleImplementationsOfInterface();
            });

            For<ICollectionPersister<BrokerageTransaction>>().Use<DuplicateBrokerageTransactionFilter>();
            For<DataContext>().Singleton().LifecycleIs(new UniquePerRequestLifecycle());
        }
    }
}