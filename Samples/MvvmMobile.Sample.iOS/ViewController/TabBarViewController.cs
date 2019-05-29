using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using MvvmMobile.Sample.iOS.ViewController.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController
{
    public class TabBarViewController : TabBarControllerBase<ITabBarViewModel>
    {
        // Private Members
        private UINavigationController[] _tabs;


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Create the tabs
            _tabs = new[]
            {
                new UINavigationController(new Nav1ViewController { Title = "Nav 1" }),
                new UINavigationController(new Nav2ViewController { Title = "Nav 2" })
            };

            // Finish up...
            ViewControllers = _tabs;
        }
    }
}