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
        public static void Init(IContainerBuilder container, IResolver resolver, Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Bootstrapper.Init(container, resolver);

            // Init Self
            container.RegisterSingleton<INavigation, AppNavigation>();

            container.Register<ILoadTabPayload>(new LoadTabPayload());
            container.Register<ICallbackPayload>(new CallbackPayload());
            container.Register<IFragmentContainerPayload>(new FragmentContainerPayload());

            // Init Navigation
            resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}