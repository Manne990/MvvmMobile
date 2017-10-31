using Android.App;
using Android.OS;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;

namespace MvvmMobile.Sample.Droid.Activities
{
    [Activity(Label = "Start", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class StartActivity : ActivityBase<IStartViewModel>
    {
        // Private Members
        private Button _button;


        // -----------------------------------------------------------------------------

        // Lifecycle
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.StartActivityLayout);

            // Controls
            _button = FindViewById<Button>(Resource.Id.button);
            _button.Click += (sender, e) => 
            {
                ViewModel.MoveNextCommand.Execute("Select a name");
            };
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _button = null;
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Name))
            {
                System.Diagnostics.Debug.WriteLine($"Name sent back: {ViewModel.Name}");
                return;
            }
        }
    }
}