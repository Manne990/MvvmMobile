using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;

namespace MvvmMobile.Droid
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

            container.Register<ILoadTabPayload, LoadTabPayload>();
            container.Register<ICallbackPayload, CallbackPayload>();
            container.Register<IFragmentContainerPayload, FragmentContainerPayload>();
        }

        public static void Init(bool useActivityTransitions = false)
        {
            // Init Core
            Core.Mvvm.Api.Init(_container);

            // Init Navigation
            ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).Init(useActivityTransitions);
        }

        public static void AddViewMapping<TViewModel, TPlatformView>() where TViewModel : IBaseViewModel where TPlatformView : IPlatformView
        {
            ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).AddViewMapping<TViewModel, TPlatformView>();
        }
    }
}