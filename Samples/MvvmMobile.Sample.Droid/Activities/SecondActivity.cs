using Android.App;
using Android.OS;
using Android.Widget;
using MvvmMobile.Droid.View;
using MvvmMobile.Sample.Core.ViewModel;
using XLabs.Ioc;

namespace MvvmMobile.Sample.Droid.Activities
{
    [Activity(Label = "Second", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SecondActivity : ActivityBase
    {
        private ISecondViewModel _viewModel;
        private TextView _titleTextView;
        private Button _firstButton;
        private Button _secondButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Init
            SetContentView(Resource.Layout.SecondActivityLayout);

            // Load View Model
            _viewModel = Resolver.Resolve<ISecondViewModel>();
            ViewModel = _viewModel;

            // Controls
            _titleTextView = FindViewById<TextView>(Resource.Id.titleTextView);
            _titleTextView.Text = string.Empty;

            _firstButton = FindViewById<Button>(Resource.Id.firstButton);
            _firstButton.Click += (sender, e) => 
            {
                // Report back
                _viewModel.NameSelectedCommand.Execute("Jonas");
            };

            _secondButton = FindViewById<Button>(Resource.Id.secondButton);
            _secondButton.Click += (sender, e) => 
            {
                // Report back
                _viewModel.NameSelectedCommand.Execute("Kalle");
            };

            // Load the payload
            _viewModel.Load(PayloadId);
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel = _viewModel;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _titleTextView = null;
            _firstButton = null;
            _secondButton = null;
        }

        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.Title))
            {
                _titleTextView.Text = _viewModel.Title;
                return;
            }
        }
    }
}