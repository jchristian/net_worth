using core.importers.persisters;
using data.models.write;
using StructureMap.Configuration.DSL;

namespace ui.web.DependencyResolution
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<BrokerageTransaction>();
                x.SingleImplementationsOfInterface();
            });

        }
    }
}