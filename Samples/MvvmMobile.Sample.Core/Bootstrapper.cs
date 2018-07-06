using MvvmMobile.Core.Common;
using MvvmMobile.Sample.Core.IoC;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel.Home;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;

namespace MvvmMobile.Sample.Core
{
    public static class Bootstrapper
    {
        public static IContainerBuilder Init()
        {
            var builder = new AutofacContainerBuilder();

            builder.Register<IMotorcyclePayload>(new MotorcyclePayload());

            builder.Register<IHomeViewModel, HomeViewModel>();

            builder.RegisterSingleton<IStartViewModel, StartViewModel>();
            builder.RegisterSingleton<IEditMotorcycleViewModel, EditMotorcycleViewModel>();

            return builder;
        }
    }
}