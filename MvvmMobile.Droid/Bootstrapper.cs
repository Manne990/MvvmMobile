using MvvmMobile.Core.Navigation;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

namespace MvvmMobile.Droid
{
    public class Bootstrapper
    {
        private readonly TinyContainer _tinyContainer;

        public Bootstrapper()
        {
            var container = TinyIoCContainer.Current;
            _tinyContainer = new TinyContainer(container);
            container.Register<IDependencyContainer>(_tinyContainer);
            Resolver.SetResolver(new TinyResolver(container));
        }

        public void Init()
        {
            var core = new Core.Bootstrapper();
            core.Init();

            _tinyContainer.RegisterSingle<INavigation, AppNavigation>();

            _tinyContainer.Register<ILoadTabPayload>(r => new LoadTabPayload());
            _tinyContainer.Register<ICallbackPayload>(r => new CallbackPayload());
            _tinyContainer.Register<IFragmentContainerPayload>(r => new FragmentContainerPayload());
        }
    }
}