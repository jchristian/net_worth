using data.models.write;
using StructureMap;

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