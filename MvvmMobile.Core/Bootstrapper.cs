using MvvmMobile.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Core
{
    public static class Bootstrapper
    {
        public static void Init()
        {
            var container = Resolver.Resolve<IDependencyContainer>();

            container.RegisterSingle<IPayloads, Payloads>();
        }
    }
}