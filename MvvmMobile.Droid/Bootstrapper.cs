using System;
using System.Collections.Generic;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace MvvmMobile.Droid
{
    public static class Bootstrapper
    {
        static readonly TinyContainer _tinyContainer;

        static Bootstrapper()
        {
            var container = TinyIoCContainer.Current;
            _tinyContainer = new TinyContainer(container);
            container.Register<IDependencyContainer>(_tinyContainer);
            Resolver.SetResolver(new TinyResolver(container));
        }

        public static void Init(Dictionary<Type, Type> viewMapper)
        {
            // Init Core
            Core.Bootstrapper.Init();

            // Init Self
            var container = Resolver.Resolve<IDependencyContainer>();

            container.RegisterSingle<INavigation, AppNavigation>();

            container.Register<ILoadTabPayload>(r => new LoadTabPayload());
            container.Register<ICallbackPayload>(r => new CallbackPayload());
            container.Register<IFragmentContainerPayload>(r => new FragmentContainerPayload());

            // Init Navigation
            Resolver.Resolve<INavigation>().Init(viewMapper);
        }
    }
}