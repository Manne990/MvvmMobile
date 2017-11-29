using System;
using System.Collections.Generic;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;

namespace MvvmMobile.iOS
{
    public static class Bootstrapper
    {
        public static void Init(IContainerBuilder container, IResolver resolver, Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Bootstrapper.Init(container, resolver);

            // Init Self
            container.RegisterSingleton<INavigation, AppNavigation>();

            // Init Navigation
            resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}