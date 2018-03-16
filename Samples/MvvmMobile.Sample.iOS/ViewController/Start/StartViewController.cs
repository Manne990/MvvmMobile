using System;
using MvvmMobile.iOS.Common;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.Model;
using MvvmMobile.Sample.Core.ViewModel;
using MvvmMobile.Sample.iOS.ViewController.Start;

namespace MvvmMobile.Sample.iOS.View
{
    [Storyboard(storyboardName:"Main", storyboardId:"StartViewController")]
    public partial class StartViewController : TableViewControllerBase<IStartViewModel>
    {
        // Private Members
        private StartTableViewSource _source;


        // -----------------------------------------------------------------------------

        // Constructors
        public StartViewController (IntPtr handle) : base (handle)
        {
        }


        // -----------------------------------------------------------------------------

        // Lifecycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _source = new StartTableViewSource(MotorcycleSelected, DeleteMotorcycle);
            TableView.Source = _source;
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            TableView?.ReloadData();
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycles))
            {
                _source?.LoadData(ViewModel.Motorcycles);
                TableView?.ReloadData();
                return;
            }
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
    }
}