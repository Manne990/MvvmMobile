using System;
using CoreGraphics;
using Foundation;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.Navigation;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel.Motorcycles;
using MvvmMobile.Sample.iOS.ViewController.Edit;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    [Storyboard(storyboardName: "Main", storyboardId: "StartContainerViewController")]
    public partial class StartContainerViewController : ViewControllerBase<IStartViewModel>, IViewControllerWithTransition
    {
        // Private Members
        private StartTableViewSource _source;
        private UITableViewCell _lastSelectedCell;


        // -----------------------------------------------------------------------------

        // Constructors
        public StartContainerViewController (IntPtr handle) : base (handle)
        {
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

            MotorcyclesTableView?.ReloadData();
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycles))
            {
                _lastSelectedCell = null;
                _source?.LoadData(ViewModel.Motorcycles);
                MotorcyclesTableView?.ReloadData();
                return;
            }
        }


        // -----------------------------------------------------------------------------

        // IViewControllerWithTransition Implementation
        public UIView GetViewForSnapshot(Type relatedViewControllerType, ViewControllerTransitioningAnimatorPresentationType transitionType)
        {
            if (transitionType == ViewControllerTransitioningAnimatorPresentationType.Present)
            {
                return _lastSelectedCell ?? NavigationController.View;
            }

            return NavigationController?.View ?? View;
        }

        public CGRect TransitionTargetRect()
        {
            if (_lastSelectedCell == null)
            {
                return View.Frame;
            }

            return _lastSelectedCell.ConvertRectToView(_lastSelectedCell.Bounds, View.Window);
        }


        // -----------------------------------------------------------------------------

        // Private Methods
        private void MotorcycleSelected(IMotorcycle motorcycle, UITableViewCell selectedCell)
        {
            _lastSelectedCell = selectedCell;
            ViewModel?.EditMotorcycleCommand.Execute(motorcycle);
        }

        partial void AddMotorcycle(NSObject sender)
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
    }
}