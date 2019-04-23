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
    [Register ("StartContainerViewController")]
    partial class StartContainerViewController
    {
        [Outlet]
        UIKit.UITableView MotorcyclesTableView { get; set; }


        [Outlet]
        UIKit.UIView SubViewOverlayView { get; set; }


        [Action ("AddMotorcycle:")]
        partial void AddMotorcycle (Foundation.NSObject sender);


        [Action ("StartNavDemo:")]
        partial void StartNavDemo (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (MotorcyclesTableView != null) {
                MotorcyclesTableView.Dispose ();
                MotorcyclesTableView = null;
            }

            if (SubViewOverlayView != null) {
                SubViewOverlayView.Dispose ();
                SubViewOverlayView = null;
            }
        }
    }
}