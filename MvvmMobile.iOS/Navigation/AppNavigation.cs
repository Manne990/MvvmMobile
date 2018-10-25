using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using MvvmMobile.Core.Common;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.View;
using UIKit;

namespace MvvmMobile.iOS.Navigation
{
    public class AppNavigation : INavigation
    {
        // Properties
        //public TabBarControllerBase TabBarController { get; set; }
        public UINavigationController NavigationController { private get; set; }
        public Dictionary<Type, Type> ViewMapperDictionary { get; private set; }


        // -----------------------------------------------------------------------------

        // Virtual Methods
        public virtual UINavigationController GetNavigationController()
        {
            return NavigationController;
        }

        public virtual Dictionary<Type, Type> GetViewMapper()
        {
            if (ViewMapperDictionary == null)
            {
                Init();
            }

            return ViewMapperDictionary;
        }

        // -----------------------------------------------------------------------------

        // Public Methods
        public void Init()
        {
            ViewMapperDictionary = new Dictionary<Type, Type>();
        }

        public UIViewController RequestView<TViewModel>() where TViewModel : IBaseViewModel
        {
            // Get the vc type
            var viewModelType = typeof(TViewModel);

            if (GetViewMapper().TryGetValue(viewModelType, out Type viewControllerType) == false)
            {
                throw new Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
            }

            // Create the vc
            var attributes = viewControllerType.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(StoryboardAttribute));
            if (attributes == null)
            {
                // Instantiate the VC
                return Activator.CreateInstance(viewControllerType) as UIViewController;
            }
            else
            {
                // Instantiate the VC from a storyboard
                var storyBoardName = attributes.ConstructorArguments[0].Value.ToString();
                var storyBoardId = attributes.ConstructorArguments[1].Value.ToString();
                var storyboard = UIStoryboard.FromName(storyBoardName, null);

                return storyboard.InstantiateViewController(storyBoardId);
            }
        }

        public void AddViewMapping<TViewModel, TPlatformView>() where TViewModel : IBaseViewModel where TPlatformView : IPlatformView
        {
            if (ViewMapperDictionary == null)
            {
                Init();
            }

            if (ViewMapperDictionary.ContainsKey(typeof(TViewModel)))
            {
                throw new System.Exception($"The viewmodel '{typeof(TViewModel).ToString()}' does already exist in view mapper!");
            }

            ViewMapperDictionary.Add(typeof(TViewModel), typeof(TPlatformView));
        }

        public void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            NavigateTo(typeof(T), parameter, callback, clearHistory);
        }

        public void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            if (viewModelType == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateTo: viewModelType was null!");
                return;
            }

            // Check the navigation controller
            if (GetNavigationController() == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateTo: Could not find a navigation controller!");
                return;
            }

            // Get the vc type
            if (GetViewMapper().TryGetValue(viewModelType, out Type viewControllerType) == false)
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
                if (frameworkVc.AsModal || clearHistory)
                {
					frameworkVc.AsModal = true;

                    frameworkVc.AsViewController().ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
                    GetNavigationController()?.PresentViewController(new UINavigationController(frameworkVc.AsViewController()), !clearHistory, null);
                    return;
                }
            }

            // Push the vc
            GetNavigationController()?.PushViewController(vc, true);
        }

        public void NavigateBack(Action done = null)
        {
            // Check the navigation controller
            if (GetNavigationController()?.VisibleViewController == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack: Could not find a navigation controller or a visible VC!");
                return;
            }

            // Get the current VC
            var currentVC = GetNavigationController().VisibleViewController as IViewControllerBase;
            if (currentVC == null)
            {
                throw new Exception("The current VC does not implement IViewControllerBase!");
            }

            // Dismiss the VC
            if (currentVC.AsModal)
            {
                GetNavigationController()?.DismissViewController(true, () => 
                {
                    done?.Invoke();
                });
            }
            else
            {
                GetNavigationController()?.PopViewController(true);
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

		public async Task NavigateBack<T>() where T : IBaseViewModel
        {
			// Check the navigation controller
            if (GetNavigationController()?.VisibleViewController == null)
            {
                System.Diagnostics.Debug.WriteLine("AppNavigation.NavigateBack<T>: Could not find a navigation controller or a visible VC!");
                return;
            }

			while (true)
			{
				// Get the current VC
                var currentVC = GetNavigationController().VisibleViewController as IViewControllerBase;
                if (currentVC == null)
                {
                    throw new Exception("The current VC does not implement IViewControllerBase!");
                }

				// Get the target vc type
                if (GetViewMapper().TryGetValue(typeof(T), out Type viewControllerType) == false)
                {
					throw new Exception($"The viewmodel '{typeof(T).ToString()}' does not exist in view mapper!");
                }

                // Check if the current VC is the target VC
                if (currentVC.GetType() == viewControllerType)
                {
                    return;
                }

                var parentTabVc = GetNavigationController().VisibleViewController.TabBarController;
                if (parentTabVc != null && parentTabVc.GetType() == viewControllerType)
                {
                    return;
                }

                // Dismiss the VC
                if (currentVC.AsModal)
                {
                    await GetNavigationController()?.DismissViewControllerAsync(false);
                }
                else
                {
                    GetNavigationController()?.PopViewController(false);
                }
			}
		}

		public async Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel
        {
			await NavigateBack<T>();

			callbackAction.Invoke(payloadId);
        }
    }
}