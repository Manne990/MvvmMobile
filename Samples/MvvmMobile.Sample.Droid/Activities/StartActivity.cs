using Android.App;
using Android.OS;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Droid.Activities
{
    [Activity(Label = "Start", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class StartActivity : ActivityBase
    {
        private IStartViewModel _viewModel;
        private Button _button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.StartActivityLayout);

            // Load View Model
            _viewModel = Resolver.Resolve<IStartViewModel>();
            ViewModel = _viewModel;

            // Controls
            _button = FindViewById<Button>(Resource.Id.button);
            _button.Click += (sender, e) => 
            {
                _viewModel.MoveNextCommand.Execute("Select a name");
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel = _viewModel;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _button = null;
        }

        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Name))
            {
                System.Diagnostics.Debug.WriteLine($"Name sent back: {_viewModel.Name}");
                return;
            }
        }
    }
}