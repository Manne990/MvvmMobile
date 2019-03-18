using Android.App;
using Android.OS;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Droid.Navigation;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Activities.Navigation
{
    [Activity(Label = "Nav Demo", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class NavStartActivity : ActivityBase<INavStartViewModel>
    {
        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.NavStartActivityLayout);

            // Toolbar
            SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar));
        }

        protected override void OnResume()
        {
            base.OnResume();

            EnableBackButton(true);

            ViewModel?.StartCommand?.Execute();
        }

        public override void OnBackPressed()
        {
            if (BackButtonEnabled == false)
            {
                return;
            }

            if (SupportFragmentManager != null && SupportFragmentManager.BackStackEntryCount <= 1)
            {
                Finish();
            }

            base.OnBackPressed();
        }
    }
}