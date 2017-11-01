using Android.App;
using Android.OS;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Droid.Activities
{
    [Activity(Label = "Second", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SecondActivity : ActivityBase<IEditMotorcycleViewModel>
    {
        //// Private Members
        //private TextView _titleTextView;
        //private Button _firstButton;
        //private Button _secondButton;


        //// -----------------------------------------------------------------------------

        //// Lifecycle
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Init
        //    SetContentView(Resource.Layout.SecondActivityLayout);

        //    // Controls
        //    _titleTextView = FindViewById<TextView>(Resource.Id.titleTextView);
        //    _titleTextView.Text = string.Empty;

        //    _firstButton = FindViewById<Button>(Resource.Id.firstButton);
        //    _firstButton.Click += (sender, e) => 
        //    {
        //        // Report back
        //        ViewModel.NameSelectedCommand.Execute("Jonas");
        //    };

        //    _secondButton = FindViewById<Button>(Resource.Id.secondButton);
        //    _secondButton.Click += (sender, e) => 
        //    {
        //        // Report back
        //        ViewModel.NameSelectedCommand.Execute("Kalle");
        //    };
        //}

        //protected override void OnDestroy()
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