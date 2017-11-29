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
        public static void Init(IContainerBuilder container, Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Bootstrapper.Init(container);

            // Init Self
            container.RegisterSingleton<INavigation, AppNavigation>();

            container.Register<ILoadTabPayload>(new LoadTabPayload());
            container.Register<ICallbackPayload>(new CallbackPayload());
            container.Register<IFragmentContainerPayload>(new FragmentContainerPayload());

            // Init Navigation
            container.Resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}