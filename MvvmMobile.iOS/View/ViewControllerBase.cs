using System;
using System.ComponentModel;
using Foundation;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.iOS.View
{
    public class ViewControllerBase<T> : UIViewController, IViewControllerBase where T : class, IBaseViewModel
    {
        // Private Members
        private bool _isFramesReady;
        private NSObject _didBecomeActiveNotificationObserver;
        private NSObject _didBecomeInActiveNotificationObserver;


        // -----------------------------------------------------------------------------

        // Constructors
        public ViewControllerBase()
        {
        }

        public ViewControllerBase(IntPtr handle) : base (handle)
        {
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel = Core.Mvvm.Api.Resolver.Resolve<T>();

            if (this is ISubViewContainerController subViewContainer)
            {
                ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).SubViewContainerController = subViewContainer;

                if (subViewContainer.SubViewContainerView != null)
                {
                    subViewContainer.SubViewOriginalConstraints = subViewContainer.SubViewContainerView.Constraints;
                }
            }

            ViewModel?.InitWithPayload(PayloadId);
        }

        public override void ViewWillLayoutSubviews()
        {
            if (_isFramesReady == false)
            {
                ViewFramesReady();
            }

            _isFramesReady = true;

            base.ViewWillLayoutSubviews();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            // Recreate the VM if needed
            ViewModel ??= ViewControllerHelper.RecreateIfNeeded(ViewModel, PayloadId);

            // Navigation
            var appNavigation = (AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>();           

            if (NavigationController != null && IsSubView == false)
            {
                appNavigation.NavigationController = NavigationController;
            }

            if (this is ISubViewContainerController subViewContainer)
            {
                appNavigation.SubViewContainerController = subViewContainer;
            }

            if (NavigationItem != null)
            {
                NavigationItem.Title = Title;
            }

            if (ViewModel != null)
            {
                ViewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                ViewModel.PropertyChanged += ViewModelPropertyChangedInternal;
            }

            ViewModel?.OnActivated();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _didBecomeActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, DidBecomeActive);
            _didBecomeInActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillResignActiveNotification, DidBecomeInactive);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            _viewModel?.OnPaused();

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
            }

            if (this is ISubViewContainerController subViewContainer)
            {
                ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).SubViewContainerController = null;
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            if (_didBecomeActiveNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_didBecomeActiveNotificationObserver);
            }

            if (_didBecomeInActiveNotificationObserver != null)
            {
                NSNotificationCenter.DefaultCenter.RemoveObserver(_didBecomeInActiveNotificationObserver);
            }
        }

        protected override void Dispose(bool disposing)
        {
            var payloads = Core.Mvvm.Api.Resolver.Resolve<IPayloads>();
            payloads?.Remove(PayloadId);

            base.Dispose(disposing);
        }


        // -----------------------------------------------------------------------------

        // Properties
        protected Guid PayloadId { get; set; }
        protected Action<Guid> CallbackAction { get; set; }
		public bool AsModal { get; set; }
        public bool SubViewHasNavBar { get; set; }
        public bool IsSubView { get; set; }

        private T _viewModel;
        protected T ViewModel
        {
            // ReSharper disable once UnusedMember.Global
            get { return _viewModel; }
            set
            {
                _viewModel = value;

                if (_viewModel == null)
                {
                    return;
                }

                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                _viewModel.PropertyChanged += ViewModelPropertyChangedInternal;

                _viewModel.OnLoaded();
                _viewModel.CallbackAction = CallbackAction;
            }
        }


        // -----------------------------------------------------------------------------

        // Virtual Methods
        public virtual void Init(IPayload payload) { }
        public virtual ITransitionAnimator LoadPresentTransitionAnimator(UIViewController sourceViewController)
        {
            return null;
        }

        public virtual ITransitionAnimator LoadDismissTransitionAnimator(UIViewController sourceViewController)
        {
            return null;
        }
        protected virtual void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e) { }
        protected virtual void ViewFramesReady() { }


        // -----------------------------------------------------------------------------

        // Public Methods
        public UIViewController AsViewController()
        {
            return this;
        }


        // -----------------------------------------------------------------------------

        // Payload and Callback Handling
        public void SetPayload(IPayload payload)
        {
            if (payload == null)
            {
                return;
            }

            // Set payload id
            PayloadId = Guid.NewGuid();

            // Add payload
            var payloads = Core.Mvvm.Api.Resolver.Resolve<IPayloads>();
            payloads.Add(PayloadId, payload);
        }

        public void SetCallback(Action<Guid> callbackAction)
        {
            if (callbackAction == null)
            {
                return;
            }

            CallbackAction = callbackAction;
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void DidBecomeActive(NSNotification obj)
        {
            // Recreate the VM if needed
            ViewModel ??= ViewControllerHelper.RecreateIfNeeded(ViewModel, PayloadId);
            ViewModel?.OnActivated();
        }

        private void DidBecomeInactive(NSNotification obj)
        {
            ViewModel?.OnPaused();
        }

        private void ViewModelPropertyChangedInternal(object sender, PropertyChangedEventArgs e)
        {
            InvokeOnMainThread(() => ViewModel_PropertyChanged(sender, e));
        }
    }
}