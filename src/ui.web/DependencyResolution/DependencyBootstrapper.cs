using StructureMap;

namespace ui.web.DependencyResolution
{
    public class DependencyBootstrapper
    {
        public static void Bootstrap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<CoreRegistry>();
                x.AddRegistry<DataRegistry>();
            });
        }
    }
}