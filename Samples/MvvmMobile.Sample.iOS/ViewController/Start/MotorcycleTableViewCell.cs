using Foundation;
using System;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    public partial class MotorcycleTableViewCell : UITableViewCell
    {
        public MotorcycleTableViewCell (IntPtr handle) : base (handle)
        {
        }

        public string Title
        {
            get { return title.Text; }
            set { title.Text = value; }
        }
    }
}