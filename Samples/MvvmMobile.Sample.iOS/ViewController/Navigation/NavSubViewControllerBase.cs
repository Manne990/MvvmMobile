using Cirrious.FluentLayouts.Touch;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class NavSubViewControllerBase<T> : ViewControllerBase<T> 
        where T : class, INavBaseViewModel
    {
        // Private Members
        private UILabel _titleLabel;
        private UIButton _nextViewButton;
        private UIButton _nextSubViewButton;
        private UIButton _prevViewButton;
        private UIButton _backButton;
        private UIButton _homeButton;

        protected UIColor BackgroundColor { private get; set; }
        protected string TitleText { private get; set; }


        // -----------------------------------------------------------------------------

        // Constructors
        public NavSubViewControllerBase()
        {
        }

        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            View.BackgroundColor = BackgroundColor;

            _titleLabel = new UILabel
            {
                Text = TitleText,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.PreferredTitle2,
                TextColor = UIColor.Black,
            };

            _nextViewButton = UIButton.FromType(UIButtonType.System);
            _nextViewButton.SetTitle("Next View", UIControlState.Normal);
            _nextViewButton.TouchUpInside += (s, e) => ViewModel?.NextViewCommand?.Execute();

            _nextSubViewButton = UIButton.FromType(UIButtonType.System);
            _nextSubViewButton.SetTitle("Next SubView", UIControlState.Normal);
            _nextSubViewButton.TouchUpInside += (s, e) => ViewModel?.NextSubViewCommand?.Execute();

            _prevViewButton = UIButton.FromType(UIButtonType.System);
            _prevViewButton.SetTitle("Prev View", UIControlState.Normal);
            _prevViewButton.TouchUpInside += (s, e) => ViewModel?.PrevViewCommand?.Execute();

            _backButton = UIButton.FromType(UIButtonType.System);
            _backButton.SetTitle("Back", UIControlState.Normal);
            _backButton.TouchUpInside += (s, e) => ViewModel?.BackCommand?.Execute();

            _homeButton = UIButton.FromType(UIButtonType.System);
            _homeButton.SetTitle("Home", UIControlState.Normal);
            _homeButton.TouchUpInside += (s, e) => ViewModel?.HomeCommand?.Execute();

            View.AddSubviews(_titleLabel,
                             _nextViewButton,
                             _nextSubViewButton,
                             _prevViewButton,
                             _backButton,
                             _homeButton);

            // Add Constraints
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _titleLabel.AtTopOf(View, 100f),
                _titleLabel.AtLeftOf(View, 8f),
                _titleLabel.WithSameWidth(View).Minus(16f));
                //_titleLabel.Height().EqualTo(40f));

            View.AddConstraints(
                _nextViewButton.Below(_titleLabel, 32f),
                _nextViewButton.AtLeftOf(View, 8f),
                _nextViewButton.WithSameWidth(View).Minus(16f),
                _nextViewButton.Height().EqualTo(40f));

            View.AddConstraints(
                _nextSubViewButton.Below(_nextViewButton, 16f),
                _nextSubViewButton.AtLeftOf(View, 8f),
                _nextSubViewButton.WithSameWidth(View).Minus(16f),
                _nextSubViewButton.Height().EqualTo(40f));

            View.AddConstraints(
                _prevViewButton.Below(_nextSubViewButton, 16f),
                _prevViewButton.AtLeftOf(View, 8f),
                _prevViewButton.WithSameWidth(View).Minus(16f),
                _prevViewButton.Height().EqualTo(40f));

            View.AddConstraints(
                _backButton.Below(_prevViewButton, 16f),
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