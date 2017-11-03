﻿using Android.App;
using Android.OS;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Droid.Activities.Start
{
    [Activity(Label = "Motorcycles", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class StartActivity : ActivityBase<IStartViewModel>
    {
        // Private Members
        private ListView _listView;
        private StartAdapter _adapter;


        // -----------------------------------------------------------------------------

        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.StartActivityLayout);

            // Controls
            _listView = FindViewById<ListView>(Resource.Id.listView);

            _adapter = new StartAdapter(DeleteMotorcycle, LayoutInflater);

            _listView.Adapter = _adapter;
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.StartActivityMenu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuAdd)
            {
                AddMotorcycle();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            base.OnResume();

            _listView.ItemClick += ListViewItemClick;
        }

        protected override void OnPause()
        {
            base.OnPause();

            _listView.ItemClick -= ListViewItemClick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _adapter = null;

            if (_listView != null)
            {
                _listView.Adapter = null;
            }

            _listView = null;
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycles))
            {
                _adapter?.LoadData(ViewModel.Motorcycles);
                return;
            }
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void ListViewItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            ViewModel?.EditMotorcycleCommand.Execute(ViewModel.Motorcycles[e.Position]);
        }

        private void AddMotorcycle()
        {
            ViewModel?.AddMotorcycleCommand.Execute();
        }

        private void DeleteMotorcycle(IMotorcycle motorcycle)
        {
            ViewModel?.DeleteMotorcycleCommand.Execute(motorcycle);
        }
    }
}