using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;

namespace MvvmMobile.Sample.Droid.Activities.Start
{
    [Activity(Label = "@string/motorcycles", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class StartActivity : ActivityBase<IStartViewModel>
    {
        // Private Members
        private FrameLayout _editMotorcycleFrame;
        private RecyclerView _listView;
        private StartAdapter _adapter;
        private FloatingActionButton _addButton;


        // -----------------------------------------------------------------------------

        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.StartActivityLayout);

            // Toolbar
            SetSupportActionBar(FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar));

            // SubView Container
            FragmentContainerId = Resource.Id.editMotorcycleFrame;
            _editMotorcycleFrame = FindViewById<FrameLayout>(FragmentContainerId);

            // Controls
            _listView = FindViewById<RecyclerView>(Resource.Id.listView);
            _listView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Vertical, false));
            _adapter = new StartAdapter(EditMotorcycle, DeleteMotorcycle, LayoutInflater);
            _listView.SetAdapter(_adapter);

            _addButton = FindViewById<FloatingActionButton>(Resource.Id.fab);
            _addButton.Click += (sender, args) => 
            {
                AddMotorcycle();
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            EnableBackButton(false);

            ShowSubViewContainer(false);
            
            _adapter?.LoadData(ViewModel.Motorcycles);
        }

        protected override void OnPause()
        {
            base.OnPause();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            if (ViewModel.IsShowingEditMotorcycleSubView == false)
            {
                MenuInflater.Inflate(Resource.Menu.StartActivityMenu, menu);
            }

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuNav)
            {
                ViewModel?.StartNavigationDemoCommand?.Execute();
                return true;
            }

            if (ViewModel.IsShowingEditMotorcycleSubView == true)
            {
                return false;
            }
                
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _adapter = null;

            if (_listView != null)
            {
                _listView.SetAdapter(null);
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

            if (e.PropertyName == nameof(ViewModel.IsShowingEditMotorcycleSubView))
            {
                ShowSubViewContainer(ViewModel.IsShowingEditMotorcycleSubView);
            }

            base.ViewModel_PropertyChanged(sender, e);
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void EditMotorcycle(IMotorcycle motorcycle)
        {
            ViewModel?.EditMotorcycleCommand.Execute(motorcycle);
        }

        private void AddMotorcycle()
        {
            ViewModel?.AddMotorcycleCommand.Execute();
        }

        private void DeleteMotorcycle(IMotorcycle motorcycle)
        {
            ViewModel?.DeleteMotorcycleCommand.Execute(motorcycle);
        }

        private void ShowSubViewContainer(bool isShowing)
        {
            _editMotorcycleFrame.Visibility = isShowing ? ViewStates.Visible : ViewStates.Gone;
            _addButton.Visibility = isShowing ? ViewStates.Gone : ViewStates.Visible;
        }
    }
}