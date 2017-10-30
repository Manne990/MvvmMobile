﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.Droid.Model;
using MvvmMobile.Droid.Navigation;
using XLabs.Ioc;

namespace MvvmMobile.Droid.View
{
    [Activity(Label = "", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class FragmentContainerActivity : ActivityBase
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
            if (FragmentManager != null && FragmentManager.BackStackEntryCount <= 1)
            {
                Finish();
            }

            base.OnBackPressed();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            // Get Payload
            var payload = Resolver.Resolve<IPayloads>().GetAndRemove<IFragmentContainerPayload>(PayloadId);

            // Load Fragment
            var app = (AppNavigation)Resolver.Resolve<INavigation>();
            app.LoadFragment(payload.FragmentType, payload.FragmentPayload);

            return base.OnCreateOptionsMenu(menu);
        }
    }
}