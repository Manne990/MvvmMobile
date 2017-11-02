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
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateTo: viewModelType was null!");
                return;
            }

            // Check the navigation controller
            if (NavigationController == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateTo: Could not find a navigation controller!");
                return;
            }

            // Get the vc type
            if (_viewMapperDictionary.TryGetValue(viewModelType, out Type viewControllerType) == false)
            {
                throw new Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
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

                vc = storyboard.InstantiateViewController(storyBoardId);
            }

            if (vc == null)
            {
                System.Diagnostics.Debug.WriteLine($"AppNavigation.NavigateTo: The ViewController for VM '{viewModelType.ToString()}' could not be loaded!");
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
                    NavigationController?.PresentViewController(new UINavigationController(frameworkVc.AsViewController()), true, null);
                    return;
                }
            }

            // Push the vc
            NavigationController?.PushViewController(vc, true);
        }

        public void NavigateBack(Action done = null)
        {
            // Check the navigation controller
            if (NavigationController?.VisibleViewController == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack: Could not find a navigation controller or a visible VC!");
                return;
            }

            // Get the current VC
            var currentVC = NavigationController.VisibleViewController as IViewControllerBase;
            if (currentVC == null)
            {
                throw new Exception("The current VC does not implement IViewControllerBase!");
            }

            // Dismiss the VC
            if (currentVC.AsModal)
            {
                NavigationController?.DismissViewController(true, () => 
                {
                    done?.Invoke();
                });
            }
            else
            {
                NavigationController?.PopViewController(true);
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
    }
}