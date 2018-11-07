using Cirrious.FluentLayouts.Touch;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class NavStartViewController : ViewControllerBase<INavStartViewModel>
    {
        // Private Members
        private UIButton _startButton;


        // -----------------------------------------------------------------------------

        // Constructors
        public NavStartViewController()
        {
            AsModal = true;
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            Title = "Nav Demo";
            View.BackgroundColor = UIColor.White;

            NavigationItem.SetRightBarButtonItem(new UIBarButtonItem("Done", UIBarButtonItemStyle.Plain, (sender, e) =>
            {
                ViewModel?.DoneCommand?.Execute();
            }), false);

            // Controls
            _startButton = UIButton.FromType(UIButtonType.System);
            _startButton.SetTitle("Start", UIControlState.Normal);
            _startButton.TouchUpInside += (s, e) =>
            {
                ViewModel?.StartCommand?.Execute();
            };

            View.AddSubview(_startButton);

            // Add Constraints
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _startButton.AtTopOf(View, 100f),
                _startButton.AtLeftOf(View, 8f),
                _startButton.WithSameWidth(View).Minus(16f),
                _startButton.Height().EqualTo(40f));
        }
    }
}