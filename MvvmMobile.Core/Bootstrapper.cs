using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core
{
    public static class Bootstrapper
    {
        public static IResolver Resolver { get; private set; }

        public static void Init(IContainerBuilder container)
        {
            Resolver = container.Resolver;

            container.RegisterSingleton<IPayloads, Payloads>();
        }
    }
}