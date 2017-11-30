using MvvmMobile.Core.Common;
using MvvmMobile.Core.ViewModel;

namespace MvvmMobile.Core
{
    public static class Bootstrapper
    {
        public static IResolver Resolver { get; private set; }

        public static void SetupIoC(IContainerBuilder container)
        {
            container.RegisterSingleton<IPayloads, Payloads>();
        }

        public static void Init(IContainerBuilder container)
        {
            Resolver = container.Resolver;
        }
    }
}