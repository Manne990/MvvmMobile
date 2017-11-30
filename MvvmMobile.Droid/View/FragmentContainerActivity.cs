using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;

namespace MvvmMobile.Droid.View
{
    [Activity(Label = "", ScreenOrientation = ScreenOrientation.Portrait)]
    internal sealed class FragmentContainerActivity : ActivityBase<IBaseViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.FragmentContainerActivityLayout);

            FragmentManager?.PopBackStackImmediate();
        }

        public override void OnBackPressed()
        {
            if (BackButtonEnabled == false)
            {
                return;
            }

            if (FragmentManager != null && FragmentManager.BackStackEntryCount <= 1)
            {
                Finish();
            }

            base.OnBackPressed();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Get Payload
            var payload = Core.Mvvm.Api.Resolver.Resolve<IPayloads>().GetAndRemove<IFragmentContainerPayload>(PayloadId);
            if (payload == null)
            {
                return base.OnCreateOptionsMenu(menu);
            }

            // Load Fragment
            var app = (AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>();

            app.FragmentContainerId = Resource.Id.fragmentContainer;
            app.LoadFragment(payload.FragmentType, payload.FragmentPayload, payload.FragmentCallback);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return false;
        }
    }
}