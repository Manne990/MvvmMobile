using Foundation;
using System;
using UIKit;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    public partial class MotorcycleTableViewCell : UITableViewCell
    {
        public MotorcycleTableViewCell (IntPtr handle) : base (handle)
        {
            BackgroundColor = UIColor.Cyan;
            Layer.CornerRadius = 10f;
        }

        public string Title
        {
            get { return title.Text; }
            set { title.Text = value; }
        }
    }
}