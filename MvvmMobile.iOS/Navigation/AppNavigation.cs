using System;
using System.Collections.Generic;
using System.Linq;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Common;
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

        public void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null)
        {
            if (viewModelType == null)
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
            if (_viewMapperDictionary.TryGetValue(viewModelType, out Type viewControllerType) == false)
            {
                //TODO: Handle Error!
                return;
            }

            // Create the vc
            UIViewController vc = null;
            var attributes = viewControllerType.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(StoryboardAttribute));
            if (attributes == null)
            {
                // Instantiate the VC
                vc = Activator.CreateInstance(viewControllerType) as UIViewController;
            }
            else
            {
                // Instantiate the VC from a storyboard
                var storyBoardName = attributes.ConstructorArguments[0].Value.ToString();
                var storyBoardId = attributes.ConstructorArguments[1].Value.ToString();
                var storyboard = UIStoryboard.FromName(storyBoardName, null);
                if (storyboard == null) 
                {
                    //TODO: Handle Error!
                    return;
                }

                vc = storyboard.InstantiateViewController(storyBoardId);
            }

            if (vc == null)
            {
                //TODO: Handle Error!
                return;
            }

            if (vc is IViewControllerBase frameworkVc)
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
                    frameworkVc.AsViewController().ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
                    result.navController.PresentViewController(new UINavigationController(frameworkVc.AsViewController()), true, null);
                    return;
                }
            }

            // Push the vc
            vc.NavigationItem.BackBarButtonItem = new UIBarButtonItem(string.Empty, UIBarButtonItemStyle.Plain, null);
            result.navController.PushViewController(vc, true);
        }

        public void NavigateBack(Action done = null)
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

        public void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null)
        {
            NavigateBack(() => 
            {
                callbackAction.Invoke(payloadId);

                done?.Invoke();
            });
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private (bool isModal, UINavigationController navController) GetCurrentNavigationController()
        {
            var rootVc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var isModal = rootVc.PresentedViewController != null;

            if (rootVc is UINavigationController)
            {
                return (isModal, rootVc as UINavigationController);
            }

            var navController = (rootVc.PresentedViewController ?? NavigationController) as UINavigationController;

            return (isModal, navController);
        }

        private bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}