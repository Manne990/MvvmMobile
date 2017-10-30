using MvvmMobile.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Core
{
    public class Bootstrapper
    {
        public void Init()
        {
            var container = Resolver.Resolve<IDependencyContainer>();

            container.RegisterSingle<IPayloads, Payloads>();
        }
    }
}