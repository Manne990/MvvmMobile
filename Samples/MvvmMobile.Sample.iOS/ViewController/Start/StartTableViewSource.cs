using System;
using System.Collections.ObjectModel;
using Foundation;
using MvvmMobile.Sample.Core.Model;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    public class StartTableViewSource : UITableViewSource
    {
        // Private Members
        private ObservableCollection<IMotorcycle> _motorcycles;
        private Action<IMotorcycle> _selectionListener;
        private Action<IMotorcycle> _deleteListener;


        // -----------------------------------------------------------------------------

        // Constructors
        public StartTableViewSource(Action<IMotorcycle> selectionListener, Action<IMotorcycle> deleteListener)
        {
            _selectionListener = selectionListener;
            _deleteListener = deleteListener;
        }


        // -----------------------------------------------------------------------------

        // Public Methods
        public void LoadData(ObservableCollection<IMotorcycle> motorcycles)
        {
            _motorcycles = motorcycles;
        }


        // -----------------------------------------------------------------------------

        // Overrides
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _motorcycles?.Count ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MotorcycleCell", indexPath) as MotorcycleTableViewCell;

            cell.Title = _motorcycles[indexPath.Row].ToString();

            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            _selectionListener?.Invoke(_motorcycles[indexPath.Row]);
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                _deleteListener?.Invoke(_motorcycles[indexPath.Row]);
            }
        }
    }
}