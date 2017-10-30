using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Runtime;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Droid.Navigation;
using MvvmMobile.Sample.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Droid
{
#if DEBUG
    [Application(Debuggable = true, ManageSpaceActivity = typeof(MainActivity))]
#else
    [Application(Debuggable = false, ManageSpaceActivity = typeof(MainActivity))]
#endif
    public class SampleApplication : Application
    {
        public SampleApplication(IntPtr handle, JniHandleOwnership transfer) : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Init
            var mvvmMobile = new MvvmMobile.Droid.Bootstrapper();
            mvvmMobile.Init();

            var bootstrapper = new Core.Bootstrapper();
            bootstrapper.Init();

            var viewMapperDictionary = new Dictionary<Type, Type>
            {
                { typeof(IStartViewModel), typeof(MainActivity) },
                { typeof(ISecondViewModel), typeof(MainActivity) }
            };

            var nav = (AppNavigation)Resolver.Resolve<INavigation>();

            nav.Init(viewMapperDictionary);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();

        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            ((AppNavigation)Resolver.Resolve<INavigation>()).Context = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            ((AppNavigation)Resolver.Resolve<INavigation>()).Context = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            ((AppNavigation)Resolver.Resolve<INavigation>()).Context = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}