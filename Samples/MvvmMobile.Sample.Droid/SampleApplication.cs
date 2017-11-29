using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmMobile.Core.Common;
using MvvmMobile.Sample.Core.ViewModel;
using MvvmMobile.Sample.Droid.Activities.Start;
using MvvmMobile.Sample.Droid.Fragments.Edit;
using TinyIoC;
using XLabs.Ioc;
using XLabs.Ioc.TinyIOC;

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

            // Init Sample Core with TinyIoC
            var builder = InitWithXlabsTinyIoc();

            // Init MvvmMobile
            MvvmMobile.Droid.Bootstrapper.Init(
                builder,
                new Dictionary<Type, Type>
                {
                    { typeof(IStartViewModel), typeof(StartActivity) },
                    //{ typeof(IEditMotorcycleViewModel), typeof(EditMotorcycleActivity) }
                    { typeof(IEditMotorcycleViewModel), typeof(EditMotorcycleFragment) }
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

        private IContainerBuilder InitWithXlabsTinyIoc()
        {
            // Init Tiny IoC
            var container = TinyIoCContainer.Current;

            container.Register<IDependencyContainer>(new TinyContainer(container));

            var resolver = new TinyResolver(container);

            // Init Sample Core
            return Core.Bootstrapper.Init(resolver);
        }
    }
}