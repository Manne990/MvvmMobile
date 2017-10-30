using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace MvvmMobile.iOS
{
    public class Bootstrapper
    {
        private readonly TinyContainer _tinyContainer;

        public Bootstrapper()
        {
            var container = TinyIoCContainer.Current;
            _tinyContainer = new TinyContainer(container);
            container.Register<IDependencyContainer>(_tinyContainer);
            Resolver.SetResolver(new TinyResolver(container));
        }

        public void Init()
        {
            var core = new Core.Bootstrapper();
            core.Init();

            _tinyContainer.RegisterSingle<INavigation, AppNavigation>();
        }
    }
}