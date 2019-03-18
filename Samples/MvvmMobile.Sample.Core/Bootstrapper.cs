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
            builder.RegisterSingleton<INav1AViewModel, Nav1AViewModel>();
            builder.RegisterSingleton<INav1BViewModel, Nav1BViewModel>();
            builder.RegisterSingleton<INav1CViewModel, Nav1CViewModel>();
            builder.RegisterSingleton<INav2ViewModel, Nav2ViewModel>();
            builder.RegisterSingleton<INav2AViewModel, Nav2AViewModel>();
            builder.RegisterSingleton<INav2BViewModel, Nav2BViewModel>();
            builder.RegisterSingleton<INav2CViewModel, Nav2CViewModel>();
            builder.RegisterSingleton<INav3ViewModel, Nav3ViewModel>();
            builder.RegisterSingleton<INav3AViewModel, Nav3AViewModel>();
            builder.RegisterSingleton<INav3BViewModel, Nav3BViewModel>();
            builder.RegisterSingleton<INav3CViewModel, Nav3CViewModel>();

            return builder;
        }
    }
}