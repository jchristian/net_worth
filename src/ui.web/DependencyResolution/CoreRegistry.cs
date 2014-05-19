using core.importers.parsers;
using StructureMap.Configuration.DSL;

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
        }
    }
}