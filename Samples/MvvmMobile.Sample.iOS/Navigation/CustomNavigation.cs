using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmMobile.Core.Navigation;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.Navigation
{
    public class CustomNavigation : AppNavigation, ICustomNavigation
    {
        public override UINavigationController GetNavigationController()
        {
            return ((AppNavigation)MvvmMobile.Core.Mvvm.Api.Resolver.Resolve<INavigation>()).GetNavigationController();
        }

        public override Dictionary<Type, Type> GetViewMapper()
        {
            return ((AppNavigation)MvvmMobile.Core.Mvvm.Api.Resolver.Resolve<INavigation>()).GetViewMapper();
        }

        public async Task NavigateToRoot()
        {
            // Check the navigation controller
            if (GetNavigationController()?.VisibleViewController == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Could not find a navigation controller or a visible VC!");
                return;
            }

            var first = true;
            while (true)
            {
                // Get the current VC
                var currentVC = GetNavigationController().VisibleViewController as IViewControllerBase;
                if (currentVC == null)
                {
                    throw new Exception("The current VC does not implement IViewControllerBase!");
                }

                var currentNativeVc = GetNavigationController().VisibleViewController;

                if (currentNativeVc == UIApplication.SharedApplication.KeyWindow.RootViewController)
                {
                    return;
                }

                if (currentNativeVc.ParentViewController == UIApplication.SharedApplication.KeyWindow.RootViewController)
                {
                    return;
                }

                // Dismiss the VC
                if (currentVC.AsModal)
                {
                    await GetNavigationController()?.DismissViewControllerAsync(first);
                }
                else
                {
                    GetNavigationController()?.PopToRootViewController(first);
                }

                first = false;
            }
        }
    }
}