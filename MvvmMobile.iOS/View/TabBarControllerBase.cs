using System;
using System.ComponentModel;
using MvvmMobile.Core.Navigation;
using MvvmMobile.Core.ViewModel;
using MvvmMobile.iOS.Navigation;
using UIKit;

namespace MvvmMobile.iOS.View
{
    public class TabBarControllerBase<T> : UITabBarController, IViewControllerBase where T : class, IBaseViewModel
    {
        // Private Members
        private AppNavigation _app;
        private UINavigationController _parentNavController;
        private bool _isFramesReady;


        // -----------------------------------------------------------------------------

        // Constructors
        public TabBarControllerBase()
        {
        }

        public TabBarControllerBase (IntPtr handle) : base (handle)
        {
        }


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
            //_app.TabBarController = this;

            // Setup ViewModel
            ViewModel = Core.Mvvm.Api.Resolver.Resolve<T>();
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

            if (NavigationController != null && IsSubView == false)
            {
                _parentNavController = NavigationController;
                ((AppNavigation)Core.Mvvm.Api.Resolver.Resolve<INavigation>()).NavigationController = NavigationController;
            }
            else if (ViewControllers != null && ViewControllers.Length > 0 && ViewControllers[SelectedIndex] is UINavigationController vc)
            {
                SetCurrentNavigationController(vc);
            }

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }

            _viewModel?.OnActivated();
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (ViewControllers != null && ViewControllers.Length > 0 && ViewControllers[0] is UINavigationController firstVc)
            {
                if (_parentNavController != null)
                {
                    _parentNavController.NavigationItem.Title = firstVc.Title;
                }

                if (NavigationItem != null)
                {
                    NavigationItem.Title = firstVc.Title;
                }
            }
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            _viewModel?.OnPaused();

            if (_viewModel != null)
            {
                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
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

                _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
                _viewModel.PropertyChanged += ViewModel_PropertyChanged;

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
        private void SetCurrentNavigationController(UINavigationController navController)
        {
            if (navController == null)
            {
                return;
            }

            _app.NavigationController = navController;

            if (NavigationItem != null)
            {
                NavigationItem.Title = navController.VisibleViewController.Title;
            }

            if (_parentNavController != null)
            {
                _parentNavController.NavigationItem.Title = navController.VisibleViewController.Title;
            }
        }
    }
}
