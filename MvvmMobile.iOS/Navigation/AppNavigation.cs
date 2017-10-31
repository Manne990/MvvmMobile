using System;
using System.Collections.Generic;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.View;
using UIKit;

namespace MvvmMobile.iOS.Navigation
{
    public class AppNavigation : INavigation
    {
        // Private Members
        private Dictionary<Type, Type> _viewMapperDictionary;


        // -----------------------------------------------------------------------------

        // Public Properties
        public TabBarControllerBase TabBarController { get; set; }
        public UINavigationController NavigationController { get; set; }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void Init(Dictionary<Type, Type> viewMapper)
        {
            _viewMapperDictionary = viewMapper;
        }

        public void NavigateTo(Type activityType, IPayload parameter = null, Action<Guid> callback = null)
        {
            if (activityType == null)
            {
                return;
            }

            // Get the navigation controller
            var result = GetCurrentNavigationController();
            if (result.navController == null)
            {
                //TODO: Handle Error!
                return;
            }

            // Get the vc type
            if (_viewMapperDictionary.TryGetValue(activityType, out Type concreteType) == false)
            {
                //TODO: Handle Error!
                return;
            }

            // Create the vc
            var vc = Activator.CreateInstance(concreteType) as UIViewController;
            if (vc == null)
            {
                //TODO: Handle Error!
                return;
            }

            if (vc is ViewControllerBase frameworkVc)
            {
                // Handle payload parameter
                if (parameter != null)
                {
                    // Set payload id
                    frameworkVc.SetPayload(parameter);
                }

                // Handle callback
                if (callback != null)
                {
                    frameworkVc.SetCallback(callback);
                }

                // Handle modal
                if (frameworkVc.AsModal)
                {
                    frameworkVc.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
                    result.navController.PresentViewController(new UINavigationController(frameworkVc), true, null);
                    return;
                }
            }

            // Push the vc
            vc.NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Plain, null);
            result.navController.PushViewController(vc, true);
        }

        public void Pop(Action done)
        {
            var result = GetCurrentNavigationController();

            if (result.isModal)
            {
                result.navController?.DismissViewController(true, () => 
                {
                    done?.Invoke();
                });
            }
            else
            {
                result.navController?.PopViewController(true);
                done?.Invoke();
            }
        }

        public void GoHome(int activateTab, Action done = null)
        {
            var rootVc = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootVc.PresentedViewController != null)
            {
                rootVc.DismissViewController(false, () =>
                {
                    GoHomeFinish(activateTab, null);
                    done?.Invoke();
                    return;
                });
                return;
            }

            GoHomeFinish(activateTab, null);
            done?.Invoke();
        }

        public void GoHome(int activateTab, Type loadSubType, Action done = null)
        {
            //REMARK: This does not always work because it could potantially start a chain of API requests that iOS does not handle that well
            var rootVc = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootVc.PresentedViewController != null)
            {
                rootVc.DismissViewController(false, () =>
                {
                    GoHomeFinish(activateTab, loadSubType);
                    done?.Invoke();
                    return;
                });
                return;
            }

            GoHomeFinish(activateTab, loadSubType);
            done?.Invoke();
        }

        public void PopAndOpenPage(Type popToActivityType, Type activityType)
        {
            var result = GetCurrentNavigationController();
            if (result.navController == null)
            {
                return;
            }

            var popConcreteType = _viewMapperDictionary[popToActivityType];

            foreach (var popVc in result.navController.ViewControllers)
            {
                if (popVc.GetType() == popConcreteType)
                {
                    result.navController?.PopToViewController(popVc, true);
                }
            }

            if (activityType == null)
            {
                return;
            }

            NavigateTo(activityType);
        }


        private void GoHomeFinish(int activateTab, Type loadSubType)
        {
            TabBarController?.SetCurrentTab(activateTab);
            NavigationController?.PopToRootViewController(false);

            if (loadSubType == null)
            {
                return;
            }

            NavigateTo(loadSubType);
        }

        private (bool isModal, UINavigationController navController) GetCurrentNavigationController()
        {
            var rootVc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var isModal = rootVc.PresentedViewController != null;
            var navController = (rootVc.PresentedViewController ?? NavigationController) as UINavigationController;

            return (isModal, navController);
        }
    }
}