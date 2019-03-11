using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmMobile.Sample.Core.Navigation;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using MvvmMobile.Sample.Droid.Activities.Edit;
using MvvmMobile.Sample.Droid.Activities.Navigation;
using MvvmMobile.Sample.Droid.Activities.Start;
using MvvmMobile.Sample.Droid.Activity.Navigation;
using MvvmMobile.Sample.Droid.Fragments.Navigation;
using MvvmMobile.Sample.Droid.Navigation;

namespace MvvmMobile.Sample.Droid
{
#if DEBUG
    [Application(Debuggable = true, ManageSpaceActivity = typeof(StartActivity))]
#else
    [Application(Debuggable = false, ManageSpaceActivity = typeof(StartActivity))]
#endif
    public class SampleApplication : Application
    {
        public SampleApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Init IoC
            var builder = Core.Bootstrapper.Init();

            MvvmMobile.Droid.Bootstrapper.SetupIoC(builder);

            builder.RegisterSingleton<ICustomNavigation, CustomNavigation>();

            builder.Build();

            // Init MvvmMobile
            MvvmMobile.Droid.Bootstrapper.Init(true);

            MvvmMobile.Droid.Bootstrapper.AddViewMapping<IStartViewModel, StartActivity>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<IEditMotorcycleViewModel, EditMotorcycleActivity>();

            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INavStartViewModel, NavStartActivity>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav1ViewModel, Nav1Activity>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav1AViewModel, Nav1AFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav1BViewModel, Nav1BFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav1CViewModel, Nav1CFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav2ViewModel, Nav2Activity>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav2AViewModel, Nav2AFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav2BViewModel, Nav2BFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav2CViewModel, Nav2CFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav3ViewModel, Nav3Activity>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav3AViewModel, Nav3AFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav3BViewModel, Nav3BFragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav3CViewModel, Nav3CFragment>();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
        }
    }
}