using System;
using System.Collections.Generic;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;

namespace MvvmMobile.iOS
{
    public static class Bootstrapper
    {
        public static void Init(IContainerBuilder container, Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Bootstrapper.Init(container);

            // Init Self
            container.RegisterSingleton<INavigation, AppNavigation>();

            // Init Navigation
            container.Resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}