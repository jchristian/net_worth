using StructureMap;

namespace ui.dependency_resolution
{
    public class DependencyBootstrapper
    {
        public static void Bootstrap()
        {
            ObjectFactory.Configure(c =>
            {
                c.AddRegistry<UIRegistry>();
            });
        }
    }
}