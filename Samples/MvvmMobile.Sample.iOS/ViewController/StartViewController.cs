using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using UIKit;
using XLabs.Ioc;

namespace MvvmMobile.Sample.iOS.ViewController
{
    public class StartViewController : ViewControllerBase
    {
        private IStartViewModel _viewModel;
        private UIButton _button;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            Title = "Start";
            View.BackgroundColor = UIColor.White;

            // Load View Model
            _viewModel = Resolver.Resolve<IStartViewModel>();
            ViewModel = _viewModel;

            // Controls
            _button = new UIButton();
            _button.SetTitle("Press Me!", UIControlState.Normal);
            _button.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _button.TouchUpInside += (sender, e) => 
            {
                _viewModel.MoveNextCommand.Execute("Select a name");
            };

            // Add Controls
            Add(_button);
        }

        protected override void ViewFramesReady()
        {
            base.ViewFramesReady();

            // Set Frames
            _button.Frame = View.Bounds;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Make sure viewmodel is attached
            ViewModel = _viewModel;
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