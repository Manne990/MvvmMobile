using System;
using MvvmMobile.iOS.View;
using MvvmMobile.Sample.Core.ViewModel;
using MvvmMobile.Sample.iOS.ViewController.Start;

namespace MvvmMobile.Sample.iOS
{
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

            _source = new StartTableViewSource();
            TableView.DataSource = _source;
        }


        // -----------------------------------------------------------------------------

        // Overrides
        protected override void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ViewModel.Motorcycles))
            {
                _source?.LoadData(ViewModel.Motorcycles);
                TableView.ReloadData();
                return;
            }
        }
    }
}