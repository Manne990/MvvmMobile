using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmMobile.Sample.Core.ViewModel;
using MvvmMobile.Sample.Droid.Activities.Edit;
using MvvmMobile.Sample.Droid.Activities.Start;

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

            builder.Build();

            // Init MvvmMobile
            MvvmMobile.Droid.Bootstrapper.Init(
                new Dictionary<Type, Type>
            {
                { typeof(IStartViewModel), typeof(StartActivity) },
                { typeof(IEditMotorcycleViewModel), typeof(EditMotorcycleActivity) }
                //{ typeof(IEditMotorcycleViewModel), typeof(EditMotorcycleFragment) }
            });
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