﻿using System;
using System.ComponentModel;
using Foundation;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.iOS.View
{
    public class CollectionViewControllerBase<T> : UICollectionViewController, IViewControllerBase where T : class, IBaseViewModel
    {
        // Private Members
        private bool _isFramesReady;
        private NSObject _didBecomeActiveNotificationObserver;


        // -----------------------------------------------------------------------------

        // Constructors
        public CollectionViewControllerBase()
        {
        }

        public CollectionViewControllerBase (IntPtr handle) : base (handle)
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

            // Handle Payload
            _viewModel?.InitWithPayload(PayloadId);

            var appNavigation = (AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>();
            appNavigation.SubViewContainerController = this as ISubViewContainerController;

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

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
                _viewModel.PropertyChanged += ViewModelPropertyChangedInternal;
            }

            _viewModel?.OnActivated();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            _didBecomeActiveNotificationObserver = NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, DidBecomeActive);
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            _viewModel?.OnPaused();

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModelPropertyChangedInternal;
            }

            if (this is ISubViewContainerController)
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
            _viewModel?.OnActivated();
        }

        private void ViewModelPropertyChangedInternal(object sender, PropertyChangedEventArgs e)
        {
            InvokeOnMainThread(() => ViewModel_PropertyChanged(sender, e));
        }
    }
}