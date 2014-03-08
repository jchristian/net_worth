using StructureMap.Configuration.DSL;

namespace ui.dependency_resolution
{
    public class UIRegistry : Registry
    {
        public UIRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<MainWindow>();
                s.SingleImplementationsOfInterface();
            });
        }
    }
}