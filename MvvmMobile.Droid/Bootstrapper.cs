using System;
using System.Collections.Generic;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
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

            container.Register<ILoadTabPayload>(new LoadTabPayload());
            container.Register<ICallbackPayload>(new CallbackPayload());
            container.Register<IFragmentContainerPayload>(new FragmentContainerPayload());
        }

        public static void Init(Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Mvvm.Api.Init(_container);

            // Init Navigation
            Core.Mvvm.Api.Resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}