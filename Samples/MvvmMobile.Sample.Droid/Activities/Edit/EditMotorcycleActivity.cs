using Android.App;
using Android.OS;
using Android.Transitions;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Droid.Activities.Edit
{
    [Activity(Label = "@string/motorcycle", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class EditMotorcycleActivity : ActivityBase<IEditMotorcycleViewModel>
    {
        // Private Members
        private TextView _brandEditText;
        private TextView _modelEditText;
        private TextView _yearEditText;


        // -----------------------------------------------------------------------------

        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.EditMotorcycleLayout);

            // Toolbar
            SetActionBar(FindViewById<Toolbar>(Resource.Id.toolbar));

            // Transitions
            Window.EnterTransition = TransitionInflater.From(this).InflateTransition(Resource.Transition.slide);

            // Controls
            _brandEditText = FindViewById<TextView>(Resource.Id.brandEditText);
            _modelEditText = FindViewById<TextView>(Resource.Id.modelEditText);
            _yearEditText = FindViewById<TextView>(Resource.Id.yearEditText);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.EditMotorcycleActivityMenu, menu);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menuDone)
            {
                ViewModel.Motorcycle.Brand = _brandEditText.Text;
                ViewModel.Motorcycle.Model = _modelEditText.Text;

                if (int.TryParse(_yearEditText.Text, out int year))
                {
                    ViewModel.Motorcycle.Year = year;
                }

                ViewModel?.SaveMotorcycleCommand.Execute();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnResume()
        {
            base.OnResume();

            EnableBackButton(true);
        }

        public override void OnBackPressed()
        {
            ViewModel?.CancelCommand.Execute();
            FinishAfterTransition();
        }

		protected override void OnDestroy()
        {
            base.OnDestroy();

            _brandEditText = null;
            _modelEditText = null;
            _yearEditText = null;
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycle))
            {
                _brandEditText.Text = ViewModel.Motorcycle.Brand;
                _modelEditText.Text = ViewModel.Motorcycle.Model;
                _yearEditText.Text = ViewModel.Motorcycle.Year.ToString();
                return;
            }
        }
    }
}