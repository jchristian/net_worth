using StructureMap;

namespace ui.web.DependencyResolution
{
    public class ContainerFactory
    {
        public static IContainer GetContainer()
        {
            return new Container(c =>
            {
                c.AddRegistry<CoreRegistry>();
                c.AddRegistry<DataRegistry>();
            });
        }
    }
}