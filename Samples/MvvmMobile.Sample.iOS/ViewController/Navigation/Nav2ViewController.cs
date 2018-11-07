using Cirrious.FluentLayouts.Touch;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class Nav2ViewController : ViewControllerBase<INav2ViewModel>
    {
        // Private Members
        private UIButton _nextButton;
        private UIButton _backButton;
        private UIButton _homeButton;


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            Title = "Nav 2";
            View.BackgroundColor = UIColor.White;

            // Controls
            _nextButton = UIButton.FromType(UIButtonType.System);
            _nextButton.SetTitle("Next", UIControlState.Normal);
            _nextButton.TouchUpInside += (s, e) =>
            {
                ViewModel?.NextCommand?.Execute();
            };

            _backButton = UIButton.FromType(UIButtonType.System);
            _backButton.SetTitle("Back", UIControlState.Normal);
            _backButton.TouchUpInside += (s, e) =>
            {
                ViewModel?.BackCommand?.Execute();
            };

            _homeButton = UIButton.FromType(UIButtonType.System);
            _homeButton.SetTitle("Home", UIControlState.Normal);
            _homeButton.TouchUpInside += (s, e) =>
            {
                ViewModel?.HomeCommand?.Execute();
            };

            View.AddSubviews(_nextButton, _backButton, _homeButton);

            // Add Constraints
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _nextButton.AtTopOf(View, 100f),
                _nextButton.AtLeftOf(View, 8f),
                _nextButton.WithSameWidth(View).Minus(16f),
                _nextButton.Height().EqualTo(40f));

            View.AddConstraints(
                _backButton.Below(_nextButton, 16f),
                _backButton.AtLeftOf(View, 8f),
                _backButton.WithSameWidth(View).Minus(16f),
                _backButton.Height().EqualTo(40f));

            View.AddConstraints(
                _homeButton.Below(_backButton, 16f),
                _homeButton.AtLeftOf(View, 8f),
                _homeButton.WithSameWidth(View).Minus(16f),
                _homeButton.Height().EqualTo(40f));
        }
    }
}