using MvvmMobile.Core.Common;
using MvvmMobile.Sample.Core.IoC;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel.Home;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

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

            builder.RegisterSingleton<INavStartViewModel, NavStartViewModel>();
            builder.RegisterSingleton<INav1ViewModel, Nav1ViewModel>();
            builder.RegisterSingleton<INav2ViewModel, Nav2ViewModel>();
            builder.RegisterSingleton<INav3ViewModel, Nav3ViewModel>();

            return builder;
        }
    }
}