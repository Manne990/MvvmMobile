using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cirrious.FluentLayouts.Touch;
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
        // Private Members
        private ISubViewContainerController _subViewContainerController;

        // -----------------------------------------------------------------------------

        // Properties
        //public TabBarControllerBase TabBarController { get; set; }
        public UINavigationController NavigationController { private get; set; }
        public Dictionary<Type, Type> ViewMapperDictionary { get; private set; }

        // REMARK: Sets by a subviewcontroller on appear and to null on disappear
        public virtual ISubViewContainerController SubViewContainerController
        {
            set
            {
                if (value != _subViewContainerController)
                {
                    _subViewContainerController = value;

                    if (_subViewContainerController == null)
                    {
                        return;
                    }

                    if (SubViewNavigationStack?.Count > 0)
                    {
                        var lastSubView = SubViewNavigationStack.Pop();

                        lastSubView.RemoveFromParentViewController();
                        InflateSubView(lastSubView);
                    }
                }
            }
        }

        private UIViewController SubViewController
        {
            get { return _subViewContainerController?.AsViewController(); }
        }

        private UIView SubViewContainer
        {
            get { return _subViewContainerController?.SubViewContainerView; }
        }

        private Stack<UIViewController> SubViewNavigationStack
        {
            get { return _subViewContainerController?.SubViewNavigationStack; }
        }

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

            var vc = InstantiateViewController(viewControllerType);
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

                    if (vc.GetType().IsSubclassOf(typeof(UITabBarController)))
                    {
                        GetNavigationController()?.PresentViewController(frameworkVc.AsViewController(), !clearHistory, null);
                        return;
                    }

                    GetNavigationController()?.PresentViewController(new UINavigationController(frameworkVc.AsViewController()), !clearHistory, null);
                    return;
                }
            }

            // Push the vc
            GetNavigationController()?.PushViewController(vc, true);
        }

        public void NavigateToSubView<T>(IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false) where T : IBaseViewModel
        {
            NavigateToSubView(typeof(T), parameter, callback, clearHistory);
        }

        public void NavigateToSubView(Type viewModelType, IPayload parameter = null, Action<Guid> callback = null, bool clearHistory = false)
        {
            if (SubViewContainer == null)
            {
                throw new Exception("SubViewController and SubViewContainer must be set!");
            }

            if (ViewMapperDictionary.TryGetValue(viewModelType, out Type viewControllerType) == false)
            {
                throw new Exception($"The viewmodel '{viewModelType.ToString()}' does not exist in view mapper!");
            }

            // Add
            var subView = InstantiateViewController(viewControllerType);
            if (subView == null)
            {
                System.Diagnostics.Debug.WriteLine($"AppNavigation.NavigateToSubView: The ViewController for VM '{viewModelType.ToString()}' could not be loaded!");
                return;
            }

            if (subView is IViewControllerBase frameworkVc)
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
            }

            InflateSubView(subView);
        }

        public void NavigateBack(Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
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

            // Check if we have SubViews
            int subViewCountThreshold = 0;
            switch (behaviour)
            {
                case BackBehaviour.CloseLastSubView:
                    subViewCountThreshold = 0;
                    break;
                case BackBehaviour.SkipFromLastSubView:
                    subViewCountThreshold = 1;
                    break;
                case BackBehaviour.FullViewsOnly:
                    subViewCountThreshold = int.MaxValue;
                    break;
            }

            if (SubViewNavigationStack?.Count > subViewCountThreshold)
            {
                // Remove the current subview from the stack
                var subView = SubViewNavigationStack.Pop();
                subView?.RemoveFromParentViewController();
                subView = null;

                if (SubViewNavigationStack.Count > 0)
                {
                    // Subviews left -> Inflate the next one
                    InflateSubView(SubViewNavigationStack.Pop());
                    done?.Invoke();
                    return;
                }

                RemoveAllSubViews();
                done?.Invoke();
                return;
            }

            // Remove all subviews
            RemoveAllSubViews();
            SubViewNavigationStack?.Clear();

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

        public void NavigateBack(Action<Guid> callbackAction, Guid payloadId, Action done = null, BackBehaviour behaviour = BackBehaviour.CloseLastSubView)
        {
            NavigateBack(() =>
            {
                callbackAction.Invoke(payloadId);
                done?.Invoke();
            }, behaviour);
        }

        public async Task NavigateBack<T>() where T : IBaseViewModel
        {
            await NavigateBack(typeof(T));
        }

        public async Task NavigateBack<T>(Action<Guid> callbackAction, Guid payloadId) where T : IBaseViewModel
        {
            await NavigateBack<T>();

            callbackAction.Invoke(payloadId);
        }


        public async Task NavigateBack(Type viewModelInterfaceType)
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
                if (GetViewMapper().TryGetValue(viewModelInterfaceType, out Type viewControllerType) == false)
                {
                    throw new Exception($"The viewmodel '{viewModelInterfaceType.ToString()}' does not exist in view mapper!");
                }

                // If the current VC has subviews then go through them first...
                if (currentVC is ISubViewContainerController subViewContainer)
                {
                    UIViewController subView = null;
                    SubViewContainerController = subViewContainer;
                    while (subViewContainer.SubViewNavigationStack.Count > 0)
                    {
                        subView = subViewContainer.SubViewNavigationStack.Pop();
                        if (subView.GetType() == viewControllerType)
                        {
                            InflateSubView(subView);
                            return;
                        }
                    }

                    if (subView != null)
                    {
                        InflateSubView(subView);
                    }
                }
                else
                {
                    SubViewContainerController = null;
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

        public async Task NavigateBack(Type viewModelInterfaceType, Action<Guid> callbackAction, Guid payloadId)
        {
            await NavigateBack(viewModelInterfaceType);

            callbackAction.Invoke(payloadId);
        }

        private void InflateSubView(UIViewController subView)
        {
            if (subView is IViewControllerBase vcBase)
            {
                vcBase.IsSubView = true;
            }

            if (subView is IViewControllerBase frameworkVc)
            {
                if (frameworkVc.SubViewHasNavBar)
                {
                    subView = new UINavigationController(subView);
                }
            }

            RemoveAllSubViews();

            SubViewController.AddChildViewController(subView);
            subView.View.TranslatesAutoresizingMaskIntoConstraints = false;
            SubViewContainer.AddSubview(subView.View);

            SubViewContainer.AddConstraints(
                subView.View.AtTopOf(SubViewContainer),
                subView.View.AtLeftOf(SubViewContainer),
                subView.View.WithSameWidth(SubViewContainer),
                subView.View.WithSameHeight(SubViewContainer));

            subView.DidMoveToParentViewController(SubViewController);

            SubViewNavigationStack.Push(subView);
        }

        private UIViewController InstantiateViewController(Type viewControllerType)
        {
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

            return vc;
        }

        private void RemoveAllSubViews()
        {
            if (SubViewContainer == null)
            {
                return;
            }

            SubViewContainer.RemoveConstraints(SubViewContainer.Constraints);

            if (_subViewContainerController?.SubViewOriginalConstraints != null)
            {
                SubViewContainer.AddConstraints(_subViewContainerController.SubViewOriginalConstraints);
            }

            for (int i = 0; i < SubViewContainer.Subviews.Length; i++)
            {
                var view = SubViewContainer.Subviews[0];
                view.RemoveFromSuperview();
                view = null;
            }
        }
    }
}