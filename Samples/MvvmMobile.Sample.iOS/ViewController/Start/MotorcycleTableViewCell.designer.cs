// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MvvmMobile.Sample.iOS.ViewController.Start
{
    [Register ("MotorcycleTableViewCell")]
    partial class MotorcycleTableViewCell
    {
        [Outlet]
        UIKit.UILabel title { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (title != null) {
                title.Dispose ();
                title = null;
            }
        }
    }
}