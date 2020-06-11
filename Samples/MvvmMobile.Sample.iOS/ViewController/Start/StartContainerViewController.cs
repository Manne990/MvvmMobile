using Foundation;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;
using MvvmMobile.Sample.iOS.ViewController.Edit;
using System;
using System.Collections.Generic;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    [Storyboard(storyboardName: "Main", storyboardId: "StartContainerViewController")]
    public partial class StartContainerViewController : ViewControllerBase<IStartViewModel>, ISubViewContainerController, IViewControllerWithTransition
    {
        // Private Members
        private StartTableViewSource _source;


        // -----------------------------------------------------------------------------

        // Constructors
        public StartContainerViewController (IntPtr handle) : base (handle)
        {
            SubViewNavigationStack = new Stack<UIViewController>();
        }

        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _source = new StartTableViewSource(MotorcycleSelected, DeleteMotorcycle);
            MotorcyclesTableView.Source = _source;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ShowSubViewContainer(false);
            MotorcyclesTableView?.ReloadData();
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycles))
            {
                _source?.LoadData(ViewModel.Motorcycles);
                MotorcyclesTableView?.ReloadData();
                return;
            }

            //if (e.PropertyName == nameof(ViewModel.IsShowingEditMotorcycleSubView))
            //{
            //    ShowSubViewContainer(ViewModel.IsShowingEditMotorcycleSubView);
            //}
        }


        // -----------------------------------------------------------------------------

        // ISubViewContainerController Implementation
        public Stack<UIViewController> SubViewNavigationStack { get; }
        public UIView SubViewContainerView { get { return SubViewOverlayView; } }
        public NSLayoutConstraint[] SubViewOriginalConstraints { get; set; }


        // -----------------------------------------------------------------------------

        // IViewControllerWithTransition Implementation
        public UIView GetViewForSnapshot()
        {
            return View;
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void MotorcycleSelected(IMotorcycle motorcycle)
        {
            ViewModel?.EditMotorcycleCommand.Execute(motorcycle);
        }

        partial void AddMotorcycle(Foundation.NSObject sender)
        {
            ViewModel?.AddMotorcycleCommand.Execute();
        }

        private void DeleteMotorcycle(IMotorcycle motorcycle)
        {
            ViewModel?.DeleteMotorcycleCommand.Execute(motorcycle);
        }

        partial void StartNavDemo(NSObject sender)
        {
            ViewModel?.StartNavigationDemoCommand?.Execute();
        }

        private void ShowSubViewContainer(bool isShowing)
        {
            SubViewContainerView.Hidden = !isShowing;
            SubViewBlockerView.Hidden = !isShowing;

            AddButton.Enabled = !isShowing;
            AddButton.TintColor = isShowing ? UIColor.Clear : null;
        }
    }
}