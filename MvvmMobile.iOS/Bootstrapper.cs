using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace MvvmMobile.iOS
{
    public static class Bootstrapper
    {
        static TinyContainer _tinyContainer;

        static Bootstrapper()
        {
            var container = TinyIoCContainer.Current;
            _tinyContainer = new TinyContainer(container);
            container.Register<IDependencyContainer>(_tinyContainer);
            Resolver.SetResolver(new TinyResolver(container));
        }

        public static void Init()
        {
            Core.Bootstrapper.Init();

            var container = Resolver.Resolve<IDependencyContainer>();

            container.RegisterSingle<INavigation, AppNavigation>();
        }
    }
}