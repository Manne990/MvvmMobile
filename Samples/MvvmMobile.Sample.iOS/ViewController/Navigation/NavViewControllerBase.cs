using Cirrious.FluentLayouts.Touch;
using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Navigation
{
    public class NavBaseViewController<T> : ViewControllerBase<T> 
        where T : class, INavBaseViewModel
    {
        // Private Members
        private UIView _containerView;
        private UILabel _titleLabel;

        protected UIColor BackgroundColor { private get; set; }

        // -----------------------------------------------------------------------------

        // Constructors
        public NavBaseViewController()
        {
            AsModal = true;
        }

        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            NavigationItem.LeftBarButtonItem = new UIBarButtonItem("Back", UIBarButtonItemStyle.Done, null);
            NavigationItem.LeftBarButtonItem.Clicked += (sender, e) => ViewModel?.BackCommand?.Execute();

            View.BackgroundColor = BackgroundColor;

            _titleLabel = new UILabel
            {
                Text = Title,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.PreferredTitle1,
                TextColor = UIColor.Black,
                //BackgroundColor = BackgroundColor
            };

            //NavigationItem.TitleView = _titleLabel;

            _containerView = new UIView { BackgroundColor = UIColor.Magenta, ClipsToBounds = true };

            View.AddSubviews(_containerView);

            // Add Constraints
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
            _containerView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                //_titleLabel.AtTopOf(View, 96f),
                //_titleLabel.AtLeftOf(View, 8f),
                //_titleLabel.WithSameWidth(View).Minus(16f),
                _containerView.AtTopOf(View, 108f),
                _containerView.AtLeftOf(View, 16f),
                _containerView.WithSameWidth(View).Minus(32f),
                _containerView.WithSameHeight(View).Minus(130f));

            ((AppNavigation)MvvmMobile.Core.Mvvm.Api.Resolver.Resolve<INavigation>()).SetSubViewContainer(this, _containerView);

            ViewModel?.NextSubViewCommand?.Execute();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ((AppNavigation)MvvmMobile.Core.Mvvm.Api.Resolver.Resolve<INavigation>()).SetSubViewContainer(this, _containerView);

        }
    }

}