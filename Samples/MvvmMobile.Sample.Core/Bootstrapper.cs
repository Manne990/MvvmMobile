using MvvmMobile.Core.Navigation;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Core
{
    public class Bootstrapper
    {
        public void Init()
        {
            var container = Resolver.Resolve<IDependencyContainer>();

            container.Register<ITitlePayload>(r => new TitlePayload());
            container.Register<IStartViewModel>(r => new StartViewModel(r.Resolve<INavigation>()));

            container.Register<INamePayload>(r => new NamePayload());
            container.Register<ISecondViewModel>(r => new SecondViewModel());
        }
    }
}