using System;
using System.Collections.Generic;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
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
            Core.Bootstrapper.SetupIoC(container);

            // Init Self
            container.RegisterSingleton<INavigation, AppNavigation>();
        }

        public static void Init(Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Bootstrapper.Init(_container);

            // Init Navigation
            Core.Bootstrapper.Resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}