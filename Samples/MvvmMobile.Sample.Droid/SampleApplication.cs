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
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav1ViewModel, Nav1Fragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav2ViewModel, Nav2Fragment>();
            MvvmMobile.Droid.Bootstrapper.AddViewMapping<INav3ViewModel, Nav3Fragment>();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}