using System;
using System.Collections.ObjectModel;
using Foundation;
using MvvmMobile.Sample.Core.Model;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    public class StartTableViewSource : UITableViewDataSource
    {
        private ObservableCollection<IMotorcycle> _motorcycles;

        public void LoadData(ObservableCollection<IMotorcycle> motorcycles)
        {
            _motorcycles = motorcycles;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _motorcycles?.Count ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MotorcycleCell", indexPath);

            cell.TextLabel.Text = _motorcycles[indexPath.Row].ToString();

            return cell;
        }
    }
}