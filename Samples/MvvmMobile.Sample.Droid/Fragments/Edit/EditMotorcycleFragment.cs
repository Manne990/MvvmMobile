using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Droid.Fragments.Edit
{
    public class EditMotorcycleFragment : FragmentBase<IEditMotorcycleViewModel>
    {
        // Private Members
        private TextView _brandEditText;
        private TextView _modelEditText;
        private TextView _yearEditText;


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Title = GetString(Resource.String.motorcycle);
            HasOptionsMenu = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.EditMotorcycleLayout, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _brandEditText = view.FindViewById<TextView>(Resource.Id.brandEditText);
            _modelEditText = view.FindViewById<TextView>(Resource.Id.modelEditText);
            _yearEditText = view.FindViewById<TextView>(Resource.Id.yearEditText);

            ParentActivity.SetSupportActionBar(view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar));
        }

        public override void OnResume()
        {
            base.OnResume();

            ParentActivity.Title = Title;
            //ParentActivity.SupportActionBar.Title = Title;
            ParentActivity.EnableBackButton(true);
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.EditMotorcycleActivityMenu, menu);

            base.OnCreateOptionsMenu(menu, inflater);
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

		protected override void OnBackPressed()
		{
            ViewModel?.CancelCommand.Execute();
		}

		public override void OnDestroyView()
        {
            base.OnDestroyView();

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