using core.events;
using core.importers.filters;
using core.importers.parsers;
using data.models.contexts;
using StructureMap;
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

            For<DataContext>().Singleton().LifecycleIs(new UniquePerRequestLifecycle());
            For<EventBus>().Singleton().LifecycleIs(new UniquePerRequestLifecycle());
        }
    }
}