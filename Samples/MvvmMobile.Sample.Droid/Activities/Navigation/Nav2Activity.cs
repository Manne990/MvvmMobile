﻿using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;

namespace MvvmMobile.Sample.Droid.Activities.Navigation
{
    [Activity(Label = "Nav Demo", MainLauncher = false, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Nav2Activity : ActivityBase<INav2ViewModel>
    {
        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.NavStartActivityLayout);
            FindViewById<LinearLayout>(Resource.Id.background).SetBackgroundColor(Color.LightBlue);
            FindViewById<TextView>(Resource.Id.title).Text = "Nav View 2";

            // Toolbar
            SetSupportActionBar(FindViewById<AndroidX.AppCompat.Widget.Toolbar>(Resource.Id.toolbar));

            FragmentContainerId = Resource.Id.fragmentContainer;
            ViewModel?.NextSubViewCommand?.Execute();
        }

        protected override void OnResume()
        {
            base.OnResume();

            EnableBackButton(true);
        }
    }
}
