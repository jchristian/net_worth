using System.Windows;
using System.Windows.Navigation;
using StructureMap;
using ui.dependency_resolution;
using ui.queries;

namespace ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnLoadCompleted(NavigationEventArgs e)
        {
            base.OnLoadCompleted(e);

            DependencyBootstrapper.Bootstrap();
            MainWindow.DataContext = ObjectFactory.GetInstance<MainWindowViewModelQuery>().Get();
        }
    }
}
