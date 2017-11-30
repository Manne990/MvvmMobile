using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.iOS.View
{
    public class TabBarControllerBase : UITabBarController
    {
        // Private Members
        private AppNavigation _app;


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Init
            _app = Core.Mvvm.Api.Resolver.Resolve<INavigation>() as AppNavigation;

            // Monitor selection of tabs
            ViewControllerSelected += (sender, e) =>
            {
                // Set the current nav controller
                if (e.ViewController is UINavigationController vc)
                {
                    SetCurrentNavigationController(vc);
                }
            };

            // Add the tabbar controller to the app
            _app.TabBarController = this;
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void SetCurrentTab(int tabIndex)
        {
            if (tabIndex < 0 || tabIndex >= ViewControllers.Length)
            {
                return;
            }

            // Set the current nav controller
            if (ViewControllers[tabIndex] is UINavigationController vc)
            {
                SetCurrentNavigationController(vc);
            }

            SelectedIndex = tabIndex;
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void SetCurrentNavigationController(UINavigationController navController)
        {
            if (navController == null)
            {
                return;
            }

            _app.NavigationController = navController;
        }
    }
}
