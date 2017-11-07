using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Core
{
    public static class Bootstrapper
    {
        public static void Init()
        {
            var container = Resolver.Resolve<IDependencyContainer>();

            container.Register<IMotorcyclePayload>(r => new MotorcyclePayload());

            container.Register<IStartViewModel, StartViewModel>();
            container.Register<IEditMotorcycleViewModel, EditMotorcycleViewModel>();
        }
    }
}