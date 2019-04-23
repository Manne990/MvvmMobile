using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.Navigation;
using UIKit;

namespace MvvmMobile.Sample.iOS.Navigation
{
    public class CustomNavigation : ICustomNavigation
    {
        AppNavigation _appNavigation;

        public CustomNavigation(INavigation navigation)
        {
            _appNavigation = navigation as AppNavigation;
        }

        public UINavigationController GetNavigationController()
        {
            return _appNavigation?.GetNavigationController();
        }

        public Dictionary<Type, Type> GetViewMapper()
        {
            return _appNavigation?.GetViewMapper();
        }

        public void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            _appNavigation?.NavigateBack(done, behaviour);
        }

        public void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            _appNavigation?.NavigateBack(callbackAction, payloadId, done, behaviour);
        }

        public async Task NavigateBack<T>() where T : IBaseViewModel
        {
            await _appNavigation?.NavigateBack<T>();
        }

        public async Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel
        {
            await _appNavigation?.NavigateBack<T>(callbackAction, payloadId);
        }

        public void NavigateTo(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            _appNavigation?.NavigateTo(viewModelType, parameter, callback, clearHistory);
        }

        public void NavigateTo<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            _appNavigation?.NavigateTo<T>(parameter, callback, clearHistory);
        }

        public void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            _appNavigation?.NavigateToSubView(viewModelType, parameter, callback, clearHistory);
        }

        public void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            _appNavigation?.NavigateToSubView<T>(parameter, callback, clearHistory);
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