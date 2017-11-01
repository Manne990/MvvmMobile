using System;
using System.Collections.ObjectModel;
using Foundation;
using MvvmMobile.Sample.Core.Model;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    public class StartTableViewSource : UITableViewSource
    {
        private ObservableCollection<IMotorcycle> _motorcycles;
        private Action<IMotorcycle> _selectionListener;

        public StartTableViewSource(Action<IMotorcycle> selectionListener)
        {
            _selectionListener = selectionListener;
        }

        public void LoadData(ObservableCollection<IMotorcycle> motorcycles)
        {
            _motorcycles = motorcycles;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _motorcycles?.Count ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MotorcycleCell", indexPath);

            cell.TextLabel.Text = _motorcycles[indexPath.Row].ToString();

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            _selectionListener?.Invoke(_motorcycles[indexPath.Row]);
        }
    }
}