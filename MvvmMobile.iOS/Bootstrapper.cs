using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Navigation;

namespace MvvmMobile.iOS
{
    public static class Bootstrapper
    {
        private static IContainerBuilder _container;

        public static void SetupIoC(IContainerBuilder container)
        {
            _container = container;

            // Init Core
            Core.Mvvm.Api.SetupIoC(container);

            // Init Self
            container.RegisterSingleton<INavigation, AppNavigation>();
        }

        public static void Init()
        {
            // Init Core
            Core.Mvvm.Api.Init(_container);

            // Init Navigation
            ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).Init();
        }

        public static void AddViewMapping<TViewModel, TPlatformView>() where TViewModel : IBaseViewModel where TPlatformView : IPlatformView
        {
            ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).AddViewMapping<TViewModel, TPlatformView>();
        }
    }
}