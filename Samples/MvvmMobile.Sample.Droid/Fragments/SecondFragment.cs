using Android.OS;
using Android.Views;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Droid.Fragments
{
    public class SecondFragment : FragmentBase<IEditMotorcycleViewModel>
    {
        //// Private Members
        //private TextView _titleTextView;
        //private Button _firstButton;
        //private Button _secondButton;


        //// -----------------------------------------------------------------------------

        //// Lifecycle
        //public override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    Title = "Second";
        //}

        //public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        //{
        //    return inflater.Inflate(Resource.Layout.SecondActivityLayout, container, false);
        //}

        //public override void OnViewCreated(View view, Bundle savedInstanceState)
        //{
        //    base.OnViewCreated(view, savedInstanceState);

        //    // Controls
        //    _titleTextView = view.FindViewById<TextView>(Resource.Id.titleTextView);
        //    _titleTextView.Text = string.Empty;

        //    _firstButton = view.FindViewById<Button>(Resource.Id.firstButton);
        //    _firstButton.Click += (sender, e) => 
        //    {
        //        // Report back
        //        ViewModel.NameSelectedCommand.Execute("Jonas");
        //    };

        //    _secondButton = view.FindViewById<Button>(Resource.Id.secondButton);
        //    _secondButton.Click += (sender, e) => 
        //    {
        //        // Report back
        //        ViewModel.NameSelectedCommand.Execute("Kalle");
        //    };
        //}

        //public override void OnResume()
        //{
        //    base.OnResume();

        //    Activity.Title = Title;
        //}

        //public override void OnDestroyView()
        //{
        //    base.OnDestroy();

        //    _titleTextView = null;
        //    _firstButton = null;
        //    _secondButton = null;
        //}


        //// -----------------------------------------------------------------------------

        //// Overrides
        //protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(ViewModel.Title))
        //    {
        //        _titleTextView.Text = ViewModel.Title;
        //        return;
        //    }
        //}
    }
}