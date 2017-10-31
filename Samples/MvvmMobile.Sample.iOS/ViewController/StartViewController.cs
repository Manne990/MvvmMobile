using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController
{
    public class StartViewController : ViewControllerBase<IStartViewModel>
    {
        // Private Members
        private UIButton _button;


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            Title = "Start";
            View.BackgroundColor = UIColor.White;

            // Controls
            _button = new UIButton();
            _button.SetTitle("Press Me!", UIControlState.Normal);
            _button.SetTitleColor(UIColor.Black, UIControlState.Normal);
            _button.TouchUpInside += (sender, e) => 
            {
                ViewModel.MoveNextCommand.Execute("Select a name");
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
            base.ViewWillAppear(animated);
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